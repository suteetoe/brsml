namespace BCTOSML
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this._login = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._targetConnectStatusTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this._targetConnectButton = new System.Windows.Forms.Button();
            this._databaseMicrosoftSql = new System.Windows.Forms.RadioButton();
            this._databasePostgres = new System.Windows.Forms.RadioButton();
            this._targetDatabaseNameTextBox = new System.Windows.Forms.TextBox();
            this._targetUserPasswordTextBox = new System.Windows.Forms.TextBox();
            this._targetUserCodeTextBox = new System.Windows.Forms.TextBox();
            this._targetServerNameTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._sourceConnectStatusTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this._sourceConnectButton = new System.Windows.Forms.Button();
            this._sourceDatabaseNameTextBox = new System.Windows.Forms.TextBox();
            this._sourceUserPasswordTextBox = new System.Windows.Forms.TextBox();
            this._sourceUserCodeTextBox = new System.Windows.Forms.TextBox();
            this._sourceServerNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._bcItemMaster = new System.Windows.Forms.TabPage();
            this._bcItemGridView = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this._truncateCheckBox = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._exportBCItemtoolStrip = new System.Windows.Forms.ToolStripButton();
            this._importSMLICtoolStrip = new System.Windows.Forms.ToolStripButton();
            this._selectTableButton = new System.Windows.Forms.ToolStripButton();
            this._item = new System.Windows.Forms.TabPage();
            this._dataGridView = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this._endDateTextBox = new System.Windows.Forms.TextBox();
            this._transferBalanceButton = new System.Windows.Forms.Button();
            this._resultTextBox = new System.Windows.Forms.TextBox();
            this._itemPrice = new System.Windows.Forms.TabPage();
            this._priceStartButton = new System.Windows.Forms.Button();
            this._ar = new System.Windows.Forms.TabPage();
            this._ap = new System.Windows.Forms.TabPage();
            this._aptruncatecheckBox = new System.Windows.Forms.CheckBox();
            this._transferAP = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this._transferAR = new System.Windows.Forms.Button();
            this._arTruncateCheckbox = new System.Windows.Forms.CheckBox();
            this._truncateArContact = new System.Windows.Forms.CheckBox();
            this._transferArContact = new System.Windows.Forms.Button();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this._apContactTruncate = new System.Windows.Forms.CheckBox();
            this._apContactTransfer = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this._login.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this._bcItemMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._bcItemGridView)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this._item.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this._itemPrice.SuspendLayout();
            this._ar.SuspendLayout();
            this._ap.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this._login);
            this.tabControl1.Controls.Add(this._bcItemMaster);
            this.tabControl1.Controls.Add(this._item);
            this.tabControl1.Controls.Add(this._itemPrice);
            this.tabControl1.Controls.Add(this._ar);
            this.tabControl1.Controls.Add(this._ap);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(716, 646);
            this.tabControl1.TabIndex = 0;
            // 
            // _login
            // 
            this._login.Controls.Add(this.groupBox2);
            this._login.Controls.Add(this.groupBox1);
            this._login.Location = new System.Drawing.Point(4, 23);
            this._login.Name = "_login";
            this._login.Padding = new System.Windows.Forms.Padding(3);
            this._login.Size = new System.Drawing.Size(708, 619);
            this._login.TabIndex = 0;
            this._login.Text = "Login";
            this._login.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._targetConnectStatusTextBox);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this._targetConnectButton);
            this.groupBox2.Controls.Add(this._databaseMicrosoftSql);
            this.groupBox2.Controls.Add(this._databasePostgres);
            this.groupBox2.Controls.Add(this._targetDatabaseNameTextBox);
            this.groupBox2.Controls.Add(this._targetUserPasswordTextBox);
            this.groupBox2.Controls.Add(this._targetUserCodeTextBox);
            this.groupBox2.Controls.Add(this._targetServerNameTextBox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(17, 198);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(672, 181);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Database (ปลายทาง)";
            // 
            // _targetConnectStatusTextBox
            // 
            this._targetConnectStatusTextBox.Enabled = false;
            this._targetConnectStatusTextBox.Location = new System.Drawing.Point(132, 145);
            this._targetConnectStatusTextBox.Name = "_targetConnectStatusTextBox";
            this._targetConnectStatusTextBox.Size = new System.Drawing.Size(442, 22);
            this._targetConnectStatusTextBox.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(26, 148);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 14);
            this.label10.TabIndex = 11;
            this.label10.Text = "Connect Status :";
            // 
            // _targetConnectButton
            // 
            this._targetConnectButton.Location = new System.Drawing.Point(580, 144);
            this._targetConnectButton.Name = "_targetConnectButton";
            this._targetConnectButton.Size = new System.Drawing.Size(75, 23);
            this._targetConnectButton.TabIndex = 10;
            this._targetConnectButton.Text = "Connect";
            this._targetConnectButton.UseVisualStyleBackColor = true;
            this._targetConnectButton.Click += new System.EventHandler(this._targetConnectButton_Click);
            // 
            // _databaseMicrosoftSql
            // 
            this._databaseMicrosoftSql.AutoSize = true;
            this._databaseMicrosoftSql.Location = new System.Drawing.Point(234, 21);
            this._databaseMicrosoftSql.Name = "_databaseMicrosoftSql";
            this._databaseMicrosoftSql.Size = new System.Drawing.Size(100, 18);
            this._databaseMicrosoftSql.TabIndex = 9;
            this._databaseMicrosoftSql.Text = "Microsoft SQL";
            this._databaseMicrosoftSql.UseVisualStyleBackColor = true;
            // 
            // _databasePostgres
            // 
            this._databasePostgres.AutoSize = true;
            this._databasePostgres.Checked = true;
            this._databasePostgres.Location = new System.Drawing.Point(132, 21);
            this._databasePostgres.Name = "_databasePostgres";
            this._databasePostgres.Size = new System.Drawing.Size(72, 18);
            this._databasePostgres.TabIndex = 8;
            this._databasePostgres.TabStop = true;
            this._databasePostgres.Text = "Postgres";
            this._databasePostgres.UseVisualStyleBackColor = true;
            // 
            // _targetDatabaseNameTextBox
            // 
            this._targetDatabaseNameTextBox.Location = new System.Drawing.Point(132, 120);
            this._targetDatabaseNameTextBox.Name = "_targetDatabaseNameTextBox";
            this._targetDatabaseNameTextBox.Size = new System.Drawing.Size(523, 22);
            this._targetDatabaseNameTextBox.TabIndex = 7;
            // 
            // _targetUserPasswordTextBox
            // 
            this._targetUserPasswordTextBox.Location = new System.Drawing.Point(132, 95);
            this._targetUserPasswordTextBox.Name = "_targetUserPasswordTextBox";
            this._targetUserPasswordTextBox.PasswordChar = '*';
            this._targetUserPasswordTextBox.Size = new System.Drawing.Size(523, 22);
            this._targetUserPasswordTextBox.TabIndex = 6;
            // 
            // _targetUserCodeTextBox
            // 
            this._targetUserCodeTextBox.Location = new System.Drawing.Point(132, 70);
            this._targetUserCodeTextBox.Name = "_targetUserCodeTextBox";
            this._targetUserCodeTextBox.Size = new System.Drawing.Size(523, 22);
            this._targetUserCodeTextBox.TabIndex = 5;
            this._targetUserCodeTextBox.Text = "postgres";
            // 
            // _targetServerNameTextBox
            // 
            this._targetServerNameTextBox.Location = new System.Drawing.Point(132, 45);
            this._targetServerNameTextBox.Name = "_targetServerNameTextBox";
            this._targetServerNameTextBox.Size = new System.Drawing.Size(523, 22);
            this._targetServerNameTextBox.TabIndex = 4;
            this._targetServerNameTextBox.Text = "localhost:8080";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 14);
            this.label5.TabIndex = 3;
            this.label5.Text = "Database Name :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(60, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 14);
            this.label6.TabIndex = 2;
            this.label6.Text = "Password :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(55, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 14);
            this.label7.TabIndex = 1;
            this.label7.Text = "User Code :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(41, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 14);
            this.label8.TabIndex = 0;
            this.label8.Text = "Server Name :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._sourceConnectStatusTextBox);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this._sourceConnectButton);
            this.groupBox1.Controls.Add(this._sourceDatabaseNameTextBox);
            this.groupBox1.Controls.Add(this._sourceUserPasswordTextBox);
            this.groupBox1.Controls.Add(this._sourceUserCodeTextBox);
            this.groupBox1.Controls.Add(this._sourceServerNameTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(17, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(672, 160);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database (ต้นทาง)";
            // 
            // _sourceConnectStatusTextBox
            // 
            this._sourceConnectStatusTextBox.Enabled = false;
            this._sourceConnectStatusTextBox.Location = new System.Drawing.Point(132, 125);
            this._sourceConnectStatusTextBox.Name = "_sourceConnectStatusTextBox";
            this._sourceConnectStatusTextBox.Size = new System.Drawing.Size(442, 22);
            this._sourceConnectStatusTextBox.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 14);
            this.label9.TabIndex = 9;
            this.label9.Text = "Connect Status :";
            // 
            // _sourceConnectButton
            // 
            this._sourceConnectButton.Location = new System.Drawing.Point(580, 124);
            this._sourceConnectButton.Name = "_sourceConnectButton";
            this._sourceConnectButton.Size = new System.Drawing.Size(75, 23);
            this._sourceConnectButton.TabIndex = 8;
            this._sourceConnectButton.Text = "Connect";
            this._sourceConnectButton.UseVisualStyleBackColor = true;
            this._sourceConnectButton.Click += new System.EventHandler(this._sourceConnectButton_Click);
            // 
            // _sourceDatabaseNameTextBox
            // 
            this._sourceDatabaseNameTextBox.Location = new System.Drawing.Point(132, 99);
            this._sourceDatabaseNameTextBox.Name = "_sourceDatabaseNameTextBox";
            this._sourceDatabaseNameTextBox.Size = new System.Drawing.Size(523, 22);
            this._sourceDatabaseNameTextBox.TabIndex = 7;
            // 
            // _sourceUserPasswordTextBox
            // 
            this._sourceUserPasswordTextBox.Location = new System.Drawing.Point(132, 74);
            this._sourceUserPasswordTextBox.Name = "_sourceUserPasswordTextBox";
            this._sourceUserPasswordTextBox.Size = new System.Drawing.Size(523, 22);
            this._sourceUserPasswordTextBox.TabIndex = 6;
            // 
            // _sourceUserCodeTextBox
            // 
            this._sourceUserCodeTextBox.Location = new System.Drawing.Point(132, 49);
            this._sourceUserCodeTextBox.Name = "_sourceUserCodeTextBox";
            this._sourceUserCodeTextBox.Size = new System.Drawing.Size(523, 22);
            this._sourceUserCodeTextBox.TabIndex = 5;
            this._sourceUserCodeTextBox.Text = "sa";
            // 
            // _sourceServerNameTextBox
            // 
            this._sourceServerNameTextBox.Location = new System.Drawing.Point(132, 24);
            this._sourceServerNameTextBox.Name = "_sourceServerNameTextBox";
            this._sourceServerNameTextBox.Size = new System.Drawing.Size(523, 22);
            this._sourceServerNameTextBox.TabIndex = 4;
            this._sourceServerNameTextBox.Text = "localhost";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "Database Name :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "User Code :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Name :";
            // 
            // _bcItemMaster
            // 
            this._bcItemMaster.Controls.Add(this._bcItemGridView);
            this._bcItemMaster.Controls.Add(this.flowLayoutPanel2);
            this._bcItemMaster.Controls.Add(this.toolStrip1);
            this._bcItemMaster.Location = new System.Drawing.Point(4, 23);
            this._bcItemMaster.Name = "_bcItemMaster";
            this._bcItemMaster.Size = new System.Drawing.Size(708, 619);
            this._bcItemMaster.TabIndex = 5;
            this._bcItemMaster.Text = "ข้อมูลสินค้า";
            this._bcItemMaster.UseVisualStyleBackColor = true;
            // 
            // _bcItemGridView
            // 
            this._bcItemGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._bcItemGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._bcItemGridView.Location = new System.Drawing.Point(0, 52);
            this._bcItemGridView.Name = "_bcItemGridView";
            this._bcItemGridView.Size = new System.Drawing.Size(708, 567);
            this._bcItemGridView.TabIndex = 2;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this._truncateCheckBox);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 25);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(708, 27);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // _truncateCheckBox
            // 
            this._truncateCheckBox.AutoSize = true;
            this._truncateCheckBox.Location = new System.Drawing.Point(3, 3);
            this._truncateCheckBox.Name = "_truncateCheckBox";
            this._truncateCheckBox.Size = new System.Drawing.Size(92, 18);
            this._truncateCheckBox.TabIndex = 0;
            this._truncateCheckBox.Text = "Truncate All";
            this._truncateCheckBox.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._exportBCItemtoolStrip,
            this._importSMLICtoolStrip,
            this._selectTableButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(708, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _exportBCItemtoolStrip
            // 
            this._exportBCItemtoolStrip.Image = ((System.Drawing.Image)(resources.GetObject("_exportBCItemtoolStrip.Image")));
            this._exportBCItemtoolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._exportBCItemtoolStrip.Name = "_exportBCItemtoolStrip";
            this._exportBCItemtoolStrip.Size = new System.Drawing.Size(127, 22);
            this._exportBCItemtoolStrip.Text = "ดึงข้อมูลสินค้าจาก BC";
            this._exportBCItemtoolStrip.Click += new System.EventHandler(this._exportBCItemtoolStrip_Click);
            // 
            // _importSMLICtoolStrip
            // 
            this._importSMLICtoolStrip.Image = ((System.Drawing.Image)(resources.GetObject("_importSMLICtoolStrip.Image")));
            this._importSMLICtoolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._importSMLICtoolStrip.Name = "_importSMLICtoolStrip";
            this._importSMLICtoolStrip.Size = new System.Drawing.Size(149, 22);
            this._importSMLICtoolStrip.Text = "โอนข้อมูลสินค้าไปยัง SML";
            this._importSMLICtoolStrip.Click += new System.EventHandler(this._importSMLICtoolStrip_Click);
            // 
            // _selectTableButton
            // 
            this._selectTableButton.Image = ((System.Drawing.Image)(resources.GetObject("_selectTableButton.Image")));
            this._selectTableButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectTableButton.Name = "_selectTableButton";
            this._selectTableButton.Size = new System.Drawing.Size(79, 22);
            this._selectTableButton.Text = "เลือกตาราง";
            this._selectTableButton.Click += new System.EventHandler(this._selectTableButton_Click);
            // 
            // _item
            // 
            this._item.Controls.Add(this._dataGridView);
            this._item.Controls.Add(this.flowLayoutPanel1);
            this._item.Location = new System.Drawing.Point(4, 23);
            this._item.Name = "_item";
            this._item.Padding = new System.Windows.Forms.Padding(3);
            this._item.Size = new System.Drawing.Size(708, 619);
            this._item.TabIndex = 1;
            this._item.Text = "สินค้า (ยอดคงเหลือ)";
            this._item.UseVisualStyleBackColor = true;
            // 
            // _dataGridView
            // 
            this._dataGridView.AllowUserToAddRows = false;
            this._dataGridView.AllowUserToDeleteRows = false;
            this._dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGridView.Location = new System.Drawing.Point(3, 33);
            this._dataGridView.Name = "_dataGridView";
            this._dataGridView.ReadOnly = true;
            this._dataGridView.Size = new System.Drawing.Size(702, 583);
            this._dataGridView.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.label11);
            this.flowLayoutPanel1.Controls.Add(this._endDateTextBox);
            this.flowLayoutPanel1.Controls.Add(this._transferBalanceButton);
            this.flowLayoutPanel1.Controls.Add(this._resultTextBox);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(702, 30);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(138, 14);
            this.label11.TabIndex = 4;
            this.label11.Text = "End Date (yyyy-mm-dd)";
            // 
            // _endDateTextBox
            // 
            this._endDateTextBox.Location = new System.Drawing.Point(147, 3);
            this._endDateTextBox.Name = "_endDateTextBox";
            this._endDateTextBox.Size = new System.Drawing.Size(158, 22);
            this._endDateTextBox.TabIndex = 3;
            this._endDateTextBox.Text = "2011-12-31";
            // 
            // _transferBalanceButton
            // 
            this._transferBalanceButton.AutoSize = true;
            this._transferBalanceButton.Location = new System.Drawing.Point(311, 3);
            this._transferBalanceButton.Name = "_transferBalanceButton";
            this._transferBalanceButton.Size = new System.Drawing.Size(97, 24);
            this._transferBalanceButton.TabIndex = 1;
            this._transferBalanceButton.Text = "โอนยอดคงเหลือ";
            this._transferBalanceButton.UseVisualStyleBackColor = true;
            this._transferBalanceButton.Click += new System.EventHandler(this._transferBalanceButton_Click);
            // 
            // _resultTextBox
            // 
            this._resultTextBox.Location = new System.Drawing.Point(414, 3);
            this._resultTextBox.Name = "_resultTextBox";
            this._resultTextBox.Size = new System.Drawing.Size(274, 22);
            this._resultTextBox.TabIndex = 2;
            // 
            // _itemPrice
            // 
            this._itemPrice.Controls.Add(this._priceStartButton);
            this._itemPrice.Location = new System.Drawing.Point(4, 23);
            this._itemPrice.Name = "_itemPrice";
            this._itemPrice.Padding = new System.Windows.Forms.Padding(3);
            this._itemPrice.Size = new System.Drawing.Size(708, 619);
            this._itemPrice.TabIndex = 4;
            this._itemPrice.Text = "ราคาสินค้า";
            this._itemPrice.UseVisualStyleBackColor = true;
            // 
            // _priceStartButton
            // 
            this._priceStartButton.Location = new System.Drawing.Point(26, 22);
            this._priceStartButton.Name = "_priceStartButton";
            this._priceStartButton.Size = new System.Drawing.Size(94, 23);
            this._priceStartButton.TabIndex = 0;
            this._priceStartButton.Text = "เริ่มโอนราคา";
            this._priceStartButton.UseVisualStyleBackColor = true;
            this._priceStartButton.Click += new System.EventHandler(this._priceStartButton_Click);
            // 
            // _ar
            // 
            this._ar.Controls.Add(this.toolStrip2);
            this._ar.Controls.Add(this.groupBox3);
            this._ar.Controls.Add(this._arTruncateCheckbox);
            this._ar.Controls.Add(this._transferAR);
            this._ar.Location = new System.Drawing.Point(4, 23);
            this._ar.Name = "_ar";
            this._ar.Size = new System.Drawing.Size(708, 619);
            this._ar.TabIndex = 2;
            this._ar.Text = "ลูกหนี้";
            this._ar.UseVisualStyleBackColor = true;
            // 
            // _ap
            // 
            this._ap.Controls.Add(this.groupBox4);
            this._ap.Controls.Add(this._aptruncatecheckBox);
            this._ap.Controls.Add(this._transferAP);
            this._ap.Location = new System.Drawing.Point(4, 23);
            this._ap.Name = "_ap";
            this._ap.Size = new System.Drawing.Size(708, 619);
            this._ap.TabIndex = 3;
            this._ap.Text = "เจ้าหนี้";
            this._ap.UseVisualStyleBackColor = true;
            // 
            // _aptruncatecheckBox
            // 
            this._aptruncatecheckBox.AutoSize = true;
            this._aptruncatecheckBox.Location = new System.Drawing.Point(26, 66);
            this._aptruncatecheckBox.Name = "_aptruncatecheckBox";
            this._aptruncatecheckBox.Size = new System.Drawing.Size(105, 18);
            this._aptruncatecheckBox.TabIndex = 3;
            this._aptruncatecheckBox.Text = "Truncate Data";
            this._aptruncatecheckBox.UseVisualStyleBackColor = true;
            // 
            // _transferAP
            // 
            this._transferAP.Location = new System.Drawing.Point(26, 22);
            this._transferAP.Name = "_transferAP";
            this._transferAP.Size = new System.Drawing.Size(94, 23);
            this._transferAP.TabIndex = 1;
            this._transferAP.Text = "เริ่มโอนเจ้าหนี้";
            this._transferAP.UseVisualStyleBackColor = true;
            this._transferAP.Click += new System.EventHandler(this._transferAP_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this._truncateArContact);
            this.groupBox3.Controls.Add(this._transferArContact);
            this.groupBox3.Location = new System.Drawing.Point(23, 82);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(674, 100);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ผู้ติดต่อลูกหนี้";
            // 
            // _transferAR
            // 
            this._transferAR.Location = new System.Drawing.Point(23, 28);
            this._transferAR.Name = "_transferAR";
            this._transferAR.Size = new System.Drawing.Size(94, 23);
            this._transferAR.TabIndex = 1;
            this._transferAR.Text = "เริ่มโอนลูกหนี้";
            this._transferAR.UseVisualStyleBackColor = true;
            this._transferAR.Click += new System.EventHandler(this._transferAR_Click);
            // 
            // _arTruncateCheckbox
            // 
            this._arTruncateCheckbox.AutoSize = true;
            this._arTruncateCheckbox.Location = new System.Drawing.Point(23, 58);
            this._arTruncateCheckbox.Name = "_arTruncateCheckbox";
            this._arTruncateCheckbox.Size = new System.Drawing.Size(105, 18);
            this._arTruncateCheckbox.TabIndex = 2;
            this._arTruncateCheckbox.Text = "Truncate Data";
            this._arTruncateCheckbox.UseVisualStyleBackColor = true;
            // 
            // _truncateArContact
            // 
            this._truncateArContact.AutoSize = true;
            this._truncateArContact.Location = new System.Drawing.Point(11, 51);
            this._truncateArContact.Name = "_truncateArContact";
            this._truncateArContact.Size = new System.Drawing.Size(105, 18);
            this._truncateArContact.TabIndex = 5;
            this._truncateArContact.Text = "Truncate Data";
            this._truncateArContact.UseVisualStyleBackColor = true;
            // 
            // _transferArContact
            // 
            this._transferArContact.Location = new System.Drawing.Point(11, 21);
            this._transferArContact.Name = "_transferArContact";
            this._transferArContact.Size = new System.Drawing.Size(138, 23);
            this._transferArContact.TabIndex = 4;
            this._transferArContact.Text = "เริ่มโอนข้อมูลผู้ติดต่อลูกหนี้";
            this._transferArContact.UseVisualStyleBackColor = true;
            this._transferArContact.Click += new System.EventHandler(this._transferArContact_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(708, 25);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "เลือกตาราง";
            this.toolStripButton1.Visible = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this._apContactTruncate);
            this.groupBox4.Controls.Add(this._apContactTransfer);
            this.groupBox4.Location = new System.Drawing.Point(26, 90);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(674, 100);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ผู้ติดต่อเจ้าหนี้";
            // 
            // _apContactTruncate
            // 
            this._apContactTruncate.AutoSize = true;
            this._apContactTruncate.Location = new System.Drawing.Point(11, 51);
            this._apContactTruncate.Name = "_apContactTruncate";
            this._apContactTruncate.Size = new System.Drawing.Size(105, 18);
            this._apContactTruncate.TabIndex = 5;
            this._apContactTruncate.Text = "Truncate Data";
            this._apContactTruncate.UseVisualStyleBackColor = true;
            // 
            // _apContactTransfer
            // 
            this._apContactTransfer.Location = new System.Drawing.Point(11, 21);
            this._apContactTransfer.Name = "_apContactTransfer";
            this._apContactTransfer.Size = new System.Drawing.Size(138, 23);
            this._apContactTransfer.TabIndex = 4;
            this._apContactTransfer.Text = "เริ่มโอนข้อมูลผู้ติดต่อลูกหนี้";
            this._apContactTransfer.UseVisualStyleBackColor = true;
            this._apContactTransfer.Click += new System.EventHandler(this._apContactTransfer_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 646);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "Form1";
            this.Text = "BC TO SML";
            this.tabControl1.ResumeLayout(false);
            this._login.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this._bcItemMaster.ResumeLayout(false);
            this._bcItemMaster.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._bcItemGridView)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this._item.ResumeLayout(false);
            this._item.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this._itemPrice.ResumeLayout(false);
            this._ar.ResumeLayout(false);
            this._ar.PerformLayout();
            this._ap.ResumeLayout(false);
            this._ap.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage _login;
        private System.Windows.Forms.TabPage _item;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox _targetConnectStatusTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button _targetConnectButton;
        private System.Windows.Forms.RadioButton _databaseMicrosoftSql;
        private System.Windows.Forms.RadioButton _databasePostgres;
        private System.Windows.Forms.TextBox _targetDatabaseNameTextBox;
        private System.Windows.Forms.TextBox _targetUserPasswordTextBox;
        private System.Windows.Forms.TextBox _targetUserCodeTextBox;
        private System.Windows.Forms.TextBox _targetServerNameTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox _sourceConnectStatusTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button _sourceConnectButton;
        private System.Windows.Forms.TextBox _sourceDatabaseNameTextBox;
        private System.Windows.Forms.TextBox _sourceUserPasswordTextBox;
        private System.Windows.Forms.TextBox _sourceUserCodeTextBox;
        private System.Windows.Forms.TextBox _sourceServerNameTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage _ar;
        private System.Windows.Forms.TabPage _ap;
        private System.Windows.Forms.DataGridView _dataGridView;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button _transferBalanceButton;
        private System.Windows.Forms.TextBox _resultTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox _endDateTextBox;
        private System.Windows.Forms.TabPage _itemPrice;
        private System.Windows.Forms.Button _priceStartButton;
        private System.Windows.Forms.TabPage _bcItemMaster;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _exportBCItemtoolStrip;
        private System.Windows.Forms.ToolStripButton _importSMLICtoolStrip;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox _truncateCheckBox;
        private System.Windows.Forms.DataGridView _bcItemGridView;
        private System.Windows.Forms.Button _transferAP;
        private System.Windows.Forms.CheckBox _aptruncatecheckBox;
        private System.Windows.Forms.ToolStripButton _selectTableButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox _truncateArContact;
        private System.Windows.Forms.Button _transferArContact;
        private System.Windows.Forms.CheckBox _arTruncateCheckbox;
        private System.Windows.Forms.Button _transferAR;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox _apContactTruncate;
        private System.Windows.Forms.Button _apContactTransfer;
    }
}

