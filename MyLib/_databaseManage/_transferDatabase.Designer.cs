namespace MyLib._databaseManage
{
    partial class _transferDatabase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_transferDatabase));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this._loadScriptButton = new System.Windows.Forms.Button();
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this._userNameTextBox = new System.Windows.Forms.TextBox();
            this._scriptTextBox = new System.Windows.Forms.TextBox();
            this._userPasswordTextBox = new System.Windows.Forms.TextBox();
            this._transferModeComboBox = new System.Windows.Forms.ComboBox();
            this._databaseComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._hostPortTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._hostNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._providerComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._insertRadioButton = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this._ignoreFieldTextBox = new System.Windows.Forms.TextBox();
            this._truncateAllTableButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this._truncateRadioButton = new System.Windows.Forms.RadioButton();
            this._createRadioButton = new System.Windows.Forms.RadioButton();
            this._targetDatabaseNamtTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this._webServiceTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._stopButton = new System.Windows.Forms.Button();
            this._startButton = new System.Windows.Forms.Button();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this._resultTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._resultGrid = new MyLib._myGrid();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this._selectAllButton = new System.Windows.Forms.Button();
            this._deSelectAllButton = new System.Windows.Forms.Button();
            this._extraWhereTextbox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this._loadScriptButton);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this._userNameTextBox);
            this.groupBox1.Controls.Add(this._scriptTextBox);
            this.groupBox1.Controls.Add(this._userPasswordTextBox);
            this.groupBox1.Controls.Add(this._transferModeComboBox);
            this.groupBox1.Controls.Add(this._databaseComboBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this._hostPortTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this._hostNameTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this._providerComboBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(401, 213);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source (ต้นทาง)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(29, 175);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Transfer Script :";
            // 
            // _loadScriptButton
            // 
            this._loadScriptButton.AutoSize = true;
            this._loadScriptButton.ImageKey = "folder_view.png";
            this._loadScriptButton.ImageList = this._imageList;
            this._loadScriptButton.Location = new System.Drawing.Point(324, 170);
            this._loadScriptButton.Name = "_loadScriptButton";
            this._loadScriptButton.Size = new System.Drawing.Size(57, 23);
            this._loadScriptButton.TabIndex = 11;
            this._loadScriptButton.Text = "Load";
            this._loadScriptButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._loadScriptButton.UseVisualStyleBackColor = true;
            this._loadScriptButton.Click += new System.EventHandler(this._loadScriptButton_Click);
            // 
            // _imageList
            // 
            this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
            this._imageList.TransparentColor = System.Drawing.Color.Transparent;
            this._imageList.Images.SetKeyName(0, "media_play_green.png");
            this._imageList.Images.SetKeyName(1, "media_stop_red.png");
            this._imageList.Images.SetKeyName(2, "selection_delete.png");
            this._imageList.Images.SetKeyName(3, "checks.png");
            this._imageList.Images.SetKeyName(4, "folder_view.png");
            this._imageList.Images.SetKeyName(5, "garbage_empty.png");
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 149);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "Transfer Mode :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(54, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Database :";
            // 
            // _userNameTextBox
            // 
            this._userNameTextBox.Location = new System.Drawing.Point(121, 70);
            this._userNameTextBox.Name = "_userNameTextBox";
            this._userNameTextBox.Size = new System.Drawing.Size(158, 21);
            this._userNameTextBox.TabIndex = 3;
            // 
            // _scriptTextBox
            // 
            this._scriptTextBox.Enabled = false;
            this._scriptTextBox.Location = new System.Drawing.Point(120, 172);
            this._scriptTextBox.Name = "_scriptTextBox";
            this._scriptTextBox.Size = new System.Drawing.Size(198, 21);
            this._scriptTextBox.TabIndex = 9;
            // 
            // _userPasswordTextBox
            // 
            this._userPasswordTextBox.Location = new System.Drawing.Point(121, 95);
            this._userPasswordTextBox.Name = "_userPasswordTextBox";
            this._userPasswordTextBox.PasswordChar = '*';
            this._userPasswordTextBox.Size = new System.Drawing.Size(158, 21);
            this._userPasswordTextBox.TabIndex = 4;
            // 
            // _transferModeComboBox
            // 
            this._transferModeComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "SML -> SML",
            "BC Account -> SML"});
            this._transferModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._transferModeComboBox.FormattingEnabled = true;
            this._transferModeComboBox.Items.AddRange(new object[] {
            "SML -> SML",
            "BC Account -> SML"});
            this._transferModeComboBox.Location = new System.Drawing.Point(120, 146);
            this._transferModeComboBox.Name = "_transferModeComboBox";
            this._transferModeComboBox.Size = new System.Drawing.Size(262, 21);
            this._transferModeComboBox.TabIndex = 8;
            this._transferModeComboBox.SelectedIndexChanged += new System.EventHandler(this._transferModeComboBox_SelectedIndexChanged);
            // 
            // _databaseComboBox
            // 
            this._databaseComboBox.FormattingEnabled = true;
            this._databaseComboBox.Location = new System.Drawing.Point(121, 120);
            this._databaseComboBox.Name = "_databaseComboBox";
            this._databaseComboBox.Size = new System.Drawing.Size(158, 21);
            this._databaseComboBox.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Username :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Password :";
            // 
            // _hostPortTextBox
            // 
            this._hostPortTextBox.Location = new System.Drawing.Point(324, 45);
            this._hostPortTextBox.Name = "_hostPortTextBox";
            this._hostPortTextBox.Size = new System.Drawing.Size(58, 21);
            this._hostPortTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(284, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Port :";
            // 
            // _hostNameTextBox
            // 
            this._hostNameTextBox.Location = new System.Drawing.Point(121, 45);
            this._hostNameTextBox.Name = "_hostNameTextBox";
            this._hostNameTextBox.Size = new System.Drawing.Size(158, 21);
            this._hostNameTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Host :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Provider :";
            // 
            // _providerComboBox
            // 
            this._providerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._providerComboBox.FormattingEnabled = true;
            this._providerComboBox.Items.AddRange(new object[] {
            "Postgres",
            "mySQL",
            "Microsoft SQL"});
            this._providerComboBox.Location = new System.Drawing.Point(121, 20);
            this._providerComboBox.Name = "_providerComboBox";
            this._providerComboBox.Size = new System.Drawing.Size(158, 21);
            this._providerComboBox.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this._extraWhereTextbox);
            this.groupBox2.Controls.Add(this._insertRadioButton);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this._ignoreFieldTextBox);
            this.groupBox2.Controls.Add(this._truncateAllTableButton);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this._truncateRadioButton);
            this.groupBox2.Controls.Add(this._createRadioButton);
            this.groupBox2.Controls.Add(this._targetDatabaseNamtTextBox);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this._webServiceTextBox);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 213);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(401, 180);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Destination (ปลายทาง)";
            // 
            // _insertRadioButton
            // 
            this._insertRadioButton.AutoSize = true;
            this._insertRadioButton.Location = new System.Drawing.Point(327, 75);
            this._insertRadioButton.Name = "_insertRadioButton";
            this._insertRadioButton.Size = new System.Drawing.Size(54, 17);
            this._insertRadioButton.TabIndex = 7;
            this._insertRadioButton.Text = "Insert";
            this._insertRadioButton.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(118, 150);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Example : [guid]";
            // 
            // _ignoreFieldTextBox
            // 
            this._ignoreFieldTextBox.Location = new System.Drawing.Point(119, 124);
            this._ignoreFieldTextBox.Name = "_ignoreFieldTextBox";
            this._ignoreFieldTextBox.Size = new System.Drawing.Size(262, 21);
            this._ignoreFieldTextBox.TabIndex = 5;
            this._ignoreFieldTextBox.Text = "[roworder][guid]";
            // 
            // _truncateAllTableButton
            // 
            this._truncateAllTableButton.AutoSize = true;
            this._truncateAllTableButton.ImageKey = "garbage_empty.png";
            this._truncateAllTableButton.ImageList = this._imageList;
            this._truncateAllTableButton.Location = new System.Drawing.Point(287, 49);
            this._truncateAllTableButton.Name = "_truncateAllTableButton";
            this._truncateAllTableButton.Size = new System.Drawing.Size(95, 23);
            this._truncateAllTableButton.TabIndex = 3;
            this._truncateAllTableButton.Text = "Truncate All";
            this._truncateAllTableButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._truncateAllTableButton.UseVisualStyleBackColor = true;
            this._truncateAllTableButton.Click += new System.EventHandler(this._truncateAllTableButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(42, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Ignore Field :";
            // 
            // _truncateRadioButton
            // 
            this._truncateRadioButton.AutoSize = true;
            this._truncateRadioButton.Checked = true;
            this._truncateRadioButton.Location = new System.Drawing.Point(218, 75);
            this._truncateRadioButton.Name = "_truncateRadioButton";
            this._truncateRadioButton.Size = new System.Drawing.Size(97, 17);
            this._truncateRadioButton.TabIndex = 3;
            this._truncateRadioButton.TabStop = true;
            this._truncateRadioButton.Text = "Truncate Table";
            this._truncateRadioButton.UseVisualStyleBackColor = true;
            // 
            // _createRadioButton
            // 
            this._createRadioButton.AutoSize = true;
            this._createRadioButton.Location = new System.Drawing.Point(120, 75);
            this._createRadioButton.Name = "_createRadioButton";
            this._createRadioButton.Size = new System.Drawing.Size(87, 17);
            this._createRadioButton.TabIndex = 2;
            this._createRadioButton.Text = "Create Table";
            this._createRadioButton.UseVisualStyleBackColor = true;
            // 
            // _targetDatabaseNamtTextBox
            // 
            this._targetDatabaseNamtTextBox.Location = new System.Drawing.Point(120, 50);
            this._targetDatabaseNamtTextBox.Name = "_targetDatabaseNamtTextBox";
            this._targetDatabaseNamtTextBox.Size = new System.Drawing.Size(159, 21);
            this._targetDatabaseNamtTextBox.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Database Name :";
            // 
            // _webServiceTextBox
            // 
            this._webServiceTextBox.Enabled = false;
            this._webServiceTextBox.Location = new System.Drawing.Point(120, 25);
            this._webServiceTextBox.Name = "_webServiceTextBox";
            this._webServiceTextBox.Size = new System.Drawing.Size(262, 21);
            this._webServiceTextBox.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(99, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Web Service URL  :";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this._stopButton);
            this.flowLayoutPanel1.Controls.Add(this._startButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 502);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(401, 29);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // _stopButton
            // 
            this._stopButton.AutoSize = true;
            this._stopButton.ImageKey = "media_stop_red.png";
            this._stopButton.ImageList = this._imageList;
            this._stopButton.Location = new System.Drawing.Point(323, 3);
            this._stopButton.Name = "_stopButton";
            this._stopButton.Size = new System.Drawing.Size(75, 23);
            this._stopButton.TabIndex = 0;
            this._stopButton.Text = "Stop";
            this._stopButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._stopButton.UseVisualStyleBackColor = true;
            this._stopButton.Click += new System.EventHandler(this._stopButton_Click);
            // 
            // _startButton
            // 
            this._startButton.AutoSize = true;
            this._startButton.ImageKey = "media_play_green.png";
            this._startButton.ImageList = this._imageList;
            this._startButton.Location = new System.Drawing.Point(242, 3);
            this._startButton.Name = "_startButton";
            this._startButton.Size = new System.Drawing.Size(75, 23);
            this._startButton.TabIndex = 2;
            this._startButton.Text = "Start";
            this._startButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._startButton.UseVisualStyleBackColor = true;
            this._startButton.Click += new System.EventHandler(this._startButton_Click);
            // 
            // _timer
            // 
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // _resultTextBox
            // 
            this._resultTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this._resultTextBox.Location = new System.Drawing.Point(0, 393);
            this._resultTextBox.Multiline = true;
            this._resultTextBox.Name = "_resultTextBox";
            this._resultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._resultTextBox.Size = new System.Drawing.Size(388, 109);
            this._resultTextBox.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._resultTextBox);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(401, 531);
            this.panel1.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(5, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._resultGrid);
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(924, 531);
            this.splitContainer1.SplitterDistance = 401;
            this.splitContainer1.TabIndex = 4;
            // 
            // _resultGrid
            // 
            this._resultGrid._extraWordShow = true;
            this._resultGrid._selectRow = -1;
            this._resultGrid.BackColor = System.Drawing.SystemColors.Window;
            this._resultGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._resultGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._resultGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._resultGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._resultGrid.IsEdit = false;
            this._resultGrid.Location = new System.Drawing.Point(0, 0);
            this._resultGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._resultGrid.Name = "_resultGrid";
            this._resultGrid.Size = new System.Drawing.Size(519, 502);
            this._resultGrid.TabIndex = 2;
            this._resultGrid.TabStop = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this._selectAllButton);
            this.flowLayoutPanel2.Controls.Add(this._deSelectAllButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 502);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(519, 29);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // _selectAllButton
            // 
            this._selectAllButton.AutoSize = true;
            this._selectAllButton.ImageKey = "checks.png";
            this._selectAllButton.ImageList = this._imageList;
            this._selectAllButton.Location = new System.Drawing.Point(439, 3);
            this._selectAllButton.Name = "_selectAllButton";
            this._selectAllButton.Size = new System.Drawing.Size(77, 23);
            this._selectAllButton.TabIndex = 0;
            this._selectAllButton.Text = "Select All";
            this._selectAllButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._selectAllButton.UseVisualStyleBackColor = true;
            this._selectAllButton.Click += new System.EventHandler(this._selectAllButton_Click);
            // 
            // _deSelectAllButton
            // 
            this._deSelectAllButton.AutoSize = true;
            this._deSelectAllButton.ImageKey = "selection_delete.png";
            this._deSelectAllButton.ImageList = this._imageList;
            this._deSelectAllButton.Location = new System.Drawing.Point(344, 3);
            this._deSelectAllButton.Name = "_deSelectAllButton";
            this._deSelectAllButton.Size = new System.Drawing.Size(89, 23);
            this._deSelectAllButton.TabIndex = 2;
            this._deSelectAllButton.Text = "Remove All";
            this._deSelectAllButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._deSelectAllButton.UseVisualStyleBackColor = true;
            this._deSelectAllButton.Click += new System.EventHandler(this._deSelectAllButton_Click);
            // 
            // _extraWhereTextbox
            // 
            this._extraWhereTextbox.Location = new System.Drawing.Point(119, 98);
            this._extraWhereTextbox.Name = "_extraWhereTextbox";
            this._extraWhereTextbox.Size = new System.Drawing.Size(269, 21);
            this._extraWhereTextbox.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(48, 101);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "Extra Filter :";
            // 
            // _transferDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 541);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_transferDatabase";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transfer Database";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox _userNameTextBox;
        private System.Windows.Forms.TextBox _userPasswordTextBox;
        private System.Windows.Forms.ComboBox _databaseComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _hostPortTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _hostNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _providerComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button _startButton;
        private System.Windows.Forms.TextBox _targetDatabaseNamtTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox _webServiceTextBox;
        private System.Windows.Forms.Label label11;
        private MyLib._myGrid _resultGrid;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button _stopButton;
        private System.Windows.Forms.Timer _timer;
        private System.Windows.Forms.TextBox _resultTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton _truncateRadioButton;
        private System.Windows.Forms.RadioButton _createRadioButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button _selectAllButton;
        private System.Windows.Forms.Button _deSelectAllButton;
        private System.Windows.Forms.TextBox _ignoreFieldTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox _transferModeComboBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ImageList _imageList;
        private System.Windows.Forms.Button _loadScriptButton;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox _scriptTextBox;
        private System.Windows.Forms.Button _truncateAllTableButton;
        private System.Windows.Forms.RadioButton _insertRadioButton;
        private System.Windows.Forms.TextBox _extraWhereTextbox;
        private System.Windows.Forms.Label label13;
    }
}