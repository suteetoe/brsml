namespace MyLib._databaseManage._importExport
{
    partial class _import
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._destinationTableGrid = new MyLib._myGrid();
            this.label1 = new System.Windows.Forms.Label();
            this._mappingGrid = new MyLib._myGrid();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._processButton = new MyLib._myButton();
            this._closeButton = new MyLib._myButton();
            this._resultTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this._databaseNameComboBox = new System.Windows.Forms.ComboBox();
            this._serverNameComboBox = new System.Windows.Forms.ComboBox();
            this._fileExploreButton = new MyLib._myButton();
            this.label7 = new System.Windows.Forms.Label();
            this._fileNameTextBox = new System.Windows.Forms.TextBox();
            this._viewDataButton = new MyLib._myButton();
            this.label6 = new System.Windows.Forms.Label();
            this._tableNameComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this._providerComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._userCodeTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this._userPasswordTextBox = new System.Windows.Forms.TextBox();
            this._connectButton = new MyLib._myButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._destinationTableGrid);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._mappingGrid);
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(964, 576);
            this.splitContainer1.SplitterDistance = 321;
            this.splitContainer1.TabIndex = 1;
            // 
            // _destinationTableGrid
            // 
            this._destinationTableGrid._extraWordShow = true;
            this._destinationTableGrid._selectRow = -1;
            this._destinationTableGrid.BackColor = System.Drawing.SystemColors.Window;
            this._destinationTableGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._destinationTableGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._destinationTableGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._destinationTableGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._destinationTableGrid.Location = new System.Drawing.Point(0, 25);
            this._destinationTableGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._destinationTableGrid.Name = "_destinationTableGrid";
            this._destinationTableGrid.Size = new System.Drawing.Size(321, 551);
            this._destinationTableGrid.TabIndex = 2;
            this._destinationTableGrid.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Destination Table";
            // 
            // _mappingGrid
            // 
            this._mappingGrid._extraWordShow = true;
            this._mappingGrid._selectRow = -1;
            this._mappingGrid.BackColor = System.Drawing.SystemColors.Window;
            this._mappingGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._mappingGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._mappingGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mappingGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._mappingGrid.Location = new System.Drawing.Point(0, 160);
            this._mappingGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._mappingGrid.Name = "_mappingGrid";
            this._mappingGrid.Size = new System.Drawing.Size(639, 387);
            this._mappingGrid.TabIndex = 3;
            this._mappingGrid.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this._processButton);
            this.flowLayoutPanel1.Controls.Add(this._closeButton);
            this.flowLayoutPanel1.Controls.Add(this._resultTextBox);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 547);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(639, 29);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // _processButton
            // 
            this._processButton.AutoSize = true;
            this._processButton.BackColor = System.Drawing.Color.Transparent;
            this._processButton.ButtonText = "ประมวลผล";
            this._processButton.Location = new System.Drawing.Point(543, 0);
            this._processButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._processButton.myImage = global::MyLib.Properties.Resources.flash;
            this._processButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._processButton.myUseVisualStyleBackColor = true;
            this._processButton.Name = "_processButton";
            this._processButton.ResourceName = "process";
            this._processButton.Size = new System.Drawing.Size(95, 25);
            this._processButton.TabIndex = 1;
            this._processButton.Text = "ประมวลผล";
            this._processButton.UseVisualStyleBackColor = false;
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.AutoSize = true;
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.ButtonText = "ปิดหน้าจอ";
            this._closeButton.Location = new System.Drawing.Point(449, 0);
            this._closeButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._closeButton.myImage = global::MyLib.Properties.Resources.exit;
            this._closeButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._closeButton.myUseVisualStyleBackColor = true;
            this._closeButton.Name = "_closeButton";
            this._closeButton.ResourceName = "screen_close";
            this._closeButton.Size = new System.Drawing.Size(92, 25);
            this._closeButton.TabIndex = 0;
            this._closeButton.Text = "ปิดหน้าจอ";
            this._closeButton.UseVisualStyleBackColor = false;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _resultTextBox
            // 
            this._resultTextBox.Location = new System.Drawing.Point(123, 3);
            this._resultTextBox.Name = "_resultTextBox";
            this._resultTextBox.Size = new System.Drawing.Size(322, 23);
            this._resultTextBox.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this._databaseNameComboBox);
            this.panel1.Controls.Add(this._serverNameComboBox);
            this.panel1.Controls.Add(this._fileExploreButton);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this._fileNameTextBox);
            this.panel1.Controls.Add(this._viewDataButton);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this._tableNameComboBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this._providerComboBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this._userCodeTextBox);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this._userPasswordTextBox);
            this.panel1.Controls.Add(this._connectButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(639, 160);
            this.panel1.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Location = new System.Drawing.Point(27, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 16);
            this.label8.TabIndex = 27;
            this.label8.Text = "Database Source :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _databaseNameComboBox
            // 
            this._databaseNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._databaseNameComboBox.Enabled = false;
            this._databaseNameComboBox.FormattingEnabled = true;
            this._databaseNameComboBox.Items.AddRange(new object[] {
            "Microsoft SQL",
            "Excel File",
            "Text File"});
            this._databaseNameComboBox.Location = new System.Drawing.Point(147, 106);
            this._databaseNameComboBox.Name = "_databaseNameComboBox";
            this._databaseNameComboBox.Size = new System.Drawing.Size(336, 24);
            this._databaseNameComboBox.TabIndex = 26;
            this._databaseNameComboBox.SelectedIndexChanged += new System.EventHandler(this._databaseNameComboBox_SelectedIndexChanged);
            // 
            // _serverNameComboBox
            // 
            this._serverNameComboBox.Enabled = false;
            this._serverNameComboBox.FormattingEnabled = true;
            this._serverNameComboBox.Location = new System.Drawing.Point(147, 56);
            this._serverNameComboBox.Name = "_serverNameComboBox";
            this._serverNameComboBox.Size = new System.Drawing.Size(336, 24);
            this._serverNameComboBox.TabIndex = 25;
            // 
            // _fileExploreButton
            // 
            this._fileExploreButton.AutoSize = true;
            this._fileExploreButton.BackColor = System.Drawing.Color.Transparent;
            this._fileExploreButton.ButtonText = "เปิดแฟ้มข้อมูล";
            this._fileExploreButton.Enabled = false;
            this._fileExploreButton.Location = new System.Drawing.Point(490, 33);
            this._fileExploreButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._fileExploreButton.myImage = global::MyLib.Properties.Resources.text_view;
            this._fileExploreButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._fileExploreButton.myUseVisualStyleBackColor = true;
            this._fileExploreButton.Name = "_fileExploreButton";
            this._fileExploreButton.ResourceName = "open_file";
            this._fileExploreButton.Size = new System.Drawing.Size(115, 25);
            this._fileExploreButton.TabIndex = 23;
            this._fileExploreButton.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Location = new System.Drawing.Point(67, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 16);
            this.label7.TabIndex = 22;
            this.label7.Text = "File Name :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _fileNameTextBox
            // 
            this._fileNameTextBox.Enabled = false;
            this._fileNameTextBox.Location = new System.Drawing.Point(147, 33);
            this._fileNameTextBox.Name = "_fileNameTextBox";
            this._fileNameTextBox.Size = new System.Drawing.Size(336, 23);
            this._fileNameTextBox.TabIndex = 21;
            // 
            // _viewDataButton
            // 
            this._viewDataButton.AutoSize = true;
            this._viewDataButton.BackColor = System.Drawing.Color.Transparent;
            this._viewDataButton.ButtonText = "แสดงข้อมูล";
            this._viewDataButton.Enabled = false;
            this._viewDataButton.Location = new System.Drawing.Point(489, 133);
            this._viewDataButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._viewDataButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._viewDataButton.myUseVisualStyleBackColor = true;
            this._viewDataButton.Name = "_viewDataButton";
            this._viewDataButton.ResourceName = "view_data";
            this._viewDataButton.Size = new System.Drawing.Size(83, 25);
            this._viewDataButton.TabIndex = 20;
            this._viewDataButton.Text = "View Data";
            this._viewDataButton.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Location = new System.Drawing.Point(48, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 16);
            this.label6.TabIndex = 19;
            this.label6.Text = "Table Source :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _tableNameComboBox
            // 
            this._tableNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._tableNameComboBox.Enabled = false;
            this._tableNameComboBox.FormattingEnabled = true;
            this._tableNameComboBox.Items.AddRange(new object[] {
            "Microsoft SQL",
            "Excel File",
            "Text File"});
            this._tableNameComboBox.Location = new System.Drawing.Point(147, 133);
            this._tableNameComboBox.Name = "_tableNameComboBox";
            this._tableNameComboBox.Size = new System.Drawing.Size(336, 24);
            this._tableNameComboBox.TabIndex = 18;
            this._tableNameComboBox.SelectedIndexChanged += new System.EventHandler(this._tableNameComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Location = new System.Drawing.Point(3, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Provider Data Source :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _providerComboBox
            // 
            this._providerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._providerComboBox.FormattingEnabled = true;
            this._providerComboBox.Items.AddRange(new object[] {
            "Microsoft SQL",
            "Excel File",
            "Text File"});
            this._providerComboBox.Location = new System.Drawing.Point(147, 8);
            this._providerComboBox.Name = "_providerComboBox";
            this._providerComboBox.Size = new System.Drawing.Size(336, 24);
            this._providerComboBox.TabIndex = 10;
            this._providerComboBox.SelectedIndexChanged += new System.EventHandler(this._providerComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(49, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Server Name :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Location = new System.Drawing.Point(82, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "User ID :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _userCodeTextBox
            // 
            this._userCodeTextBox.Enabled = false;
            this._userCodeTextBox.Location = new System.Drawing.Point(147, 81);
            this._userCodeTextBox.Name = "_userCodeTextBox";
            this._userCodeTextBox.Size = new System.Drawing.Size(126, 23);
            this._userCodeTextBox.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Location = new System.Drawing.Point(279, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "Password :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _userPasswordTextBox
            // 
            this._userPasswordTextBox.Enabled = false;
            this._userPasswordTextBox.Location = new System.Drawing.Point(357, 81);
            this._userPasswordTextBox.Name = "_userPasswordTextBox";
            this._userPasswordTextBox.PasswordChar = '*';
            this._userPasswordTextBox.Size = new System.Drawing.Size(126, 23);
            this._userPasswordTextBox.TabIndex = 16;
            // 
            // _connectButton
            // 
            this._connectButton.AutoSize = true;
            this._connectButton.BackColor = System.Drawing.Color.Transparent;
            this._connectButton.ButtonText = "เชื่อมต่อ";
            this._connectButton.Enabled = false;
            this._connectButton.Location = new System.Drawing.Point(489, 81);
            this._connectButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._connectButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._connectButton.myUseVisualStyleBackColor = true;
            this._connectButton.Name = "_connectButton";
            this._connectButton.ResourceName = "connect";
            this._connectButton.Size = new System.Drawing.Size(65, 25);
            this._connectButton.TabIndex = 17;
            this._connectButton.Text = "Connect";
            this._connectButton.UseVisualStyleBackColor = false;
            this._connectButton.Click += new System.EventHandler(this._connectButton_Click);
            // 
            // _import
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "_import";
            this.Size = new System.Drawing.Size(964, 576);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _providerComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _userCodeTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox _userPasswordTextBox;
        private _myButton _connectButton;
        private _myButton _viewDataButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox _tableNameComboBox;
        private _myButton _fileExploreButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox _fileNameTextBox;
        private _myGrid _destinationTableGrid;
        private _myGrid _mappingGrid;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private _myButton _processButton;
        private _myButton _closeButton;
        private System.Windows.Forms.ComboBox _serverNameComboBox;
        private System.Windows.Forms.ComboBox _databaseNameComboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox _resultTextBox;
    }
}
