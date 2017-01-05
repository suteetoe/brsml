namespace SMLGetPrice
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._postgreSqlConnectStatus = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._dataCenterConnectStatus = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._compareButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._updatePriceButton = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this._dataPage = new System.Windows.Forms.TabPage();
            this._dataGridView = new System.Windows.Forms.DataGridView();
            this._barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._itemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._itemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._unitCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._unitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._priceOld = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._priceNew = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this._searchText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this._processLabel = new System.Windows.Forms.Label();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this._connectPage = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this._dataCenterProviderTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._dataCenterBranchCodeTextBox = new System.Windows.Forms.TextBox();
            this._dataCenterConnectStatusTextBox = new System.Windows.Forms.TextBox();
            this._smlConnectStatusTextBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this._dataCenterConnectButton = new System.Windows.Forms.Button();
            this._dataCenterUserTextBox = new System.Windows.Forms.TextBox();
            this._dataCenterDatabaseNameTextBox = new System.Windows.Forms.TextBox();
            this._dataCenterPasswordTextBox = new System.Windows.Forms.TextBox();
            this._dataCenterUrlTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this._smlConnectButton = new System.Windows.Forms.Button();
            this._smlUserTextBox = new System.Windows.Forms.TextBox();
            this._smlDatabaseNameTextBox = new System.Windows.Forms.TextBox();
            this._smlPasswordTextBox = new System.Windows.Forms.TextBox();
            this._smlServerTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this._dataPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this._connectPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._postgreSqlConnectStatus,
            this.toolStripSeparator2,
            this._dataCenterConnectStatus,
            this.toolStripSeparator1,
            this._compareButton,
            this.toolStripSeparator3,
            this._updatePriceButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1045, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _postgreSqlConnectStatus
            // 
            this._postgreSqlConnectStatus.Name = "_postgreSqlConnectStatus";
            this._postgreSqlConnectStatus.Size = new System.Drawing.Size(147, 22);
            this._postgreSqlConnectStatus.Text = "PostgeSQL Connect Status";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _dataCenterConnectStatus
            // 
            this._dataCenterConnectStatus.Name = "_dataCenterConnectStatus";
            this._dataCenterConnectStatus.Size = new System.Drawing.Size(152, 22);
            this._dataCenterConnectStatus.Text = "Data Center Connect Status";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _compareButton
            // 
            this._compareButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._compareButton.Image = ((System.Drawing.Image)(resources.GetObject("_compareButton.Image")));
            this._compareButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._compareButton.Name = "_compareButton";
            this._compareButton.Size = new System.Drawing.Size(75, 22);
            this._compareButton.Text = "ตรวจสอบราคา";
            this._compareButton.Click += new System.EventHandler(this._compareButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // _updatePriceButton
            // 
            this._updatePriceButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._updatePriceButton.Image = ((System.Drawing.Image)(resources.GetObject("_updatePriceButton.Image")));
            this._updatePriceButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._updatePriceButton.Name = "_updatePriceButton";
            this._updatePriceButton.Size = new System.Drawing.Size(108, 22);
            this._updatePriceButton.Text = "เปลี่ยนให้เป็นราคาใหม่";
            this._updatePriceButton.Click += new System.EventHandler(this._updatePriceButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this._dataPage);
            this.tabControl1.Controls.Add(this._connectPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1045, 651);
            this.tabControl1.TabIndex = 7;
            // 
            // _dataPage
            // 
            this._dataPage.Controls.Add(this._dataGridView);
            this._dataPage.Controls.Add(this.panel2);
            this._dataPage.Controls.Add(this.panel1);
            this._dataPage.Location = new System.Drawing.Point(4, 24);
            this._dataPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._dataPage.Name = "_dataPage";
            this._dataPage.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._dataPage.Size = new System.Drawing.Size(1037, 623);
            this._dataPage.TabIndex = 0;
            this._dataPage.Text = "Data";
            this._dataPage.UseVisualStyleBackColor = true;
            // 
            // _dataGridView
            // 
            this._dataGridView.AllowUserToAddRows = false;
            this._dataGridView.AllowUserToDeleteRows = false;
            this._dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._barcode,
            this._itemCode,
            this._itemName,
            this._unitCode,
            this._unitName,
            this._priceOld,
            this._priceNew});
            this._dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGridView.Location = new System.Drawing.Point(4, 32);
            this._dataGridView.Name = "_dataGridView";
            this._dataGridView.ReadOnly = true;
            this._dataGridView.RowHeadersVisible = false;
            this._dataGridView.RowTemplate.Height = 28;
            this._dataGridView.Size = new System.Drawing.Size(1029, 563);
            this._dataGridView.TabIndex = 0;
            // 
            // _barcode
            // 
            this._barcode.FillWeight = 10F;
            this._barcode.HeaderText = "BARCODE";
            this._barcode.Name = "_barcode";
            this._barcode.ReadOnly = true;
            // 
            // _itemCode
            // 
            this._itemCode.FillWeight = 10F;
            this._itemCode.HeaderText = "รหัสสินค้า";
            this._itemCode.Name = "_itemCode";
            this._itemCode.ReadOnly = true;
            // 
            // _itemName
            // 
            this._itemName.FillWeight = 40F;
            this._itemName.HeaderText = "ชื่อสินค้า";
            this._itemName.Name = "_itemName";
            this._itemName.ReadOnly = true;
            // 
            // _unitCode
            // 
            this._unitCode.FillWeight = 10F;
            this._unitCode.HeaderText = "หน่วยนับ";
            this._unitCode.Name = "_unitCode";
            this._unitCode.ReadOnly = true;
            // 
            // _unitName
            // 
            this._unitName.FillWeight = 10F;
            this._unitName.HeaderText = "ชื่อหน่วยนับ";
            this._unitName.Name = "_unitName";
            this._unitName.ReadOnly = true;
            // 
            // _priceOld
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            this._priceOld.DefaultCellStyle = dataGridViewCellStyle1;
            this._priceOld.FillWeight = 10F;
            this._priceOld.HeaderText = "ราคาเดิม";
            this._priceOld.Name = "_priceOld";
            this._priceOld.ReadOnly = true;
            // 
            // _priceNew
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            this._priceNew.DefaultCellStyle = dataGridViewCellStyle2;
            this._priceNew.FillWeight = 10F;
            this._priceNew.HeaderText = "ราคาใหม่";
            this._priceNew.Name = "_priceNew";
            this._priceNew.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._searchText);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1029, 29);
            this.panel2.TabIndex = 2;
            // 
            // _searchText
            // 
            this._searchText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._searchText.Location = new System.Drawing.Point(50, 4);
            this._searchText.Name = "_searchText";
            this._searchText.Size = new System.Drawing.Size(975, 21);
            this._searchText.TabIndex = 1;
            this._searchText.KeyUp += new System.Windows.Forms.KeyEventHandler(this._searchText_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "ค้นหา :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._processLabel);
            this.panel1.Controls.Add(this._progressBar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(4, 595);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1029, 25);
            this.panel1.TabIndex = 1;
            // 
            // _processLabel
            // 
            this._processLabel.AutoSize = true;
            this._processLabel.Location = new System.Drawing.Point(396, 7);
            this._processLabel.Name = "_processLabel";
            this._processLabel.Size = new System.Drawing.Size(0, 15);
            this._processLabel.TabIndex = 1;
            // 
            // _progressBar
            // 
            this._progressBar.Location = new System.Drawing.Point(4, 4);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(386, 18);
            this._progressBar.TabIndex = 0;
            // 
            // _connectPage
            // 
            this._connectPage.Controls.Add(this.label2);
            this._connectPage.Controls.Add(this._dataCenterProviderTextBox);
            this._connectPage.Controls.Add(this.label1);
            this._connectPage.Controls.Add(this._dataCenterBranchCodeTextBox);
            this._connectPage.Controls.Add(this._dataCenterConnectStatusTextBox);
            this._connectPage.Controls.Add(this._smlConnectStatusTextBox);
            this._connectPage.Controls.Add(this.label15);
            this._connectPage.Controls.Add(this.label14);
            this._connectPage.Controls.Add(this._dataCenterConnectButton);
            this._connectPage.Controls.Add(this._dataCenterUserTextBox);
            this._connectPage.Controls.Add(this._dataCenterDatabaseNameTextBox);
            this._connectPage.Controls.Add(this._dataCenterPasswordTextBox);
            this._connectPage.Controls.Add(this._dataCenterUrlTextBox);
            this._connectPage.Controls.Add(this.label9);
            this._connectPage.Controls.Add(this.label10);
            this._connectPage.Controls.Add(this.label11);
            this._connectPage.Controls.Add(this.label12);
            this._connectPage.Controls.Add(this.label13);
            this._connectPage.Controls.Add(this._smlConnectButton);
            this._connectPage.Controls.Add(this._smlUserTextBox);
            this._connectPage.Controls.Add(this._smlDatabaseNameTextBox);
            this._connectPage.Controls.Add(this._smlPasswordTextBox);
            this._connectPage.Controls.Add(this._smlServerTextBox);
            this._connectPage.Controls.Add(this.label8);
            this._connectPage.Controls.Add(this.label7);
            this._connectPage.Controls.Add(this.label6);
            this._connectPage.Controls.Add(this.label5);
            this._connectPage.Controls.Add(this.label4);
            this._connectPage.Location = new System.Drawing.Point(4, 24);
            this._connectPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._connectPage.Name = "_connectPage";
            this._connectPage.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._connectPage.Size = new System.Drawing.Size(1037, 623);
            this._connectPage.TabIndex = 1;
            this._connectPage.Text = "Connect";
            this._connectPage.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 392);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 15);
            this.label2.TabIndex = 52;
            this.label2.Text = "Provider Name :";
            // 
            // _dataCenterProviderTextBox
            // 
            this._dataCenterProviderTextBox.Location = new System.Drawing.Point(147, 389);
            this._dataCenterProviderTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._dataCenterProviderTextBox.Name = "_dataCenterProviderTextBox";
            this._dataCenterProviderTextBox.Size = new System.Drawing.Size(705, 21);
            this._dataCenterProviderTextBox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 447);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 50;
            this.label1.Text = "รหัสสาขา :";
            // 
            // _dataCenterBranchCodeTextBox
            // 
            this._dataCenterBranchCodeTextBox.Location = new System.Drawing.Point(147, 446);
            this._dataCenterBranchCodeTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._dataCenterBranchCodeTextBox.Name = "_dataCenterBranchCodeTextBox";
            this._dataCenterBranchCodeTextBox.Size = new System.Drawing.Size(705, 21);
            this._dataCenterBranchCodeTextBox.TabIndex = 10;
            // 
            // _dataCenterConnectStatusTextBox
            // 
            this._dataCenterConnectStatusTextBox.Location = new System.Drawing.Point(147, 477);
            this._dataCenterConnectStatusTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._dataCenterConnectStatusTextBox.Name = "_dataCenterConnectStatusTextBox";
            this._dataCenterConnectStatusTextBox.ReadOnly = true;
            this._dataCenterConnectStatusTextBox.Size = new System.Drawing.Size(705, 21);
            this._dataCenterConnectStatusTextBox.TabIndex = 48;
            // 
            // _smlConnectStatusTextBox
            // 
            this._smlConnectStatusTextBox.Location = new System.Drawing.Point(147, 193);
            this._smlConnectStatusTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._smlConnectStatusTextBox.Name = "_smlConnectStatusTextBox";
            this._smlConnectStatusTextBox.ReadOnly = true;
            this._smlConnectStatusTextBox.Size = new System.Drawing.Size(705, 21);
            this._smlConnectStatusTextBox.TabIndex = 47;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(39, 482);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(100, 15);
            this.label15.TabIndex = 46;
            this.label15.Text = "สถานะการเชื่อมต่อ :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(39, 196);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 15);
            this.label14.TabIndex = 45;
            this.label14.Text = "สถานะการเชื่อมต่อ :";
            // 
            // _dataCenterConnectButton
            // 
            this._dataCenterConnectButton.Location = new System.Drawing.Point(879, 479);
            this._dataCenterConnectButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._dataCenterConnectButton.Name = "_dataCenterConnectButton";
            this._dataCenterConnectButton.Size = new System.Drawing.Size(84, 25);
            this._dataCenterConnectButton.TabIndex = 11;
            this._dataCenterConnectButton.Text = "เชื่อมต่อ";
            this._dataCenterConnectButton.UseVisualStyleBackColor = true;
            this._dataCenterConnectButton.Click += new System.EventHandler(this._dataCenterConnectButton_Click);
            // 
            // _dataCenterUserTextBox
            // 
            this._dataCenterUserTextBox.Location = new System.Drawing.Point(147, 332);
            this._dataCenterUserTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._dataCenterUserTextBox.Name = "_dataCenterUserTextBox";
            this._dataCenterUserTextBox.Size = new System.Drawing.Size(705, 21);
            this._dataCenterUserTextBox.TabIndex = 6;
            // 
            // _dataCenterDatabaseNameTextBox
            // 
            this._dataCenterDatabaseNameTextBox.Location = new System.Drawing.Point(147, 416);
            this._dataCenterDatabaseNameTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._dataCenterDatabaseNameTextBox.Name = "_dataCenterDatabaseNameTextBox";
            this._dataCenterDatabaseNameTextBox.Size = new System.Drawing.Size(705, 21);
            this._dataCenterDatabaseNameTextBox.TabIndex = 9;
            // 
            // _dataCenterPasswordTextBox
            // 
            this._dataCenterPasswordTextBox.Location = new System.Drawing.Point(147, 362);
            this._dataCenterPasswordTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._dataCenterPasswordTextBox.Name = "_dataCenterPasswordTextBox";
            this._dataCenterPasswordTextBox.Size = new System.Drawing.Size(705, 21);
            this._dataCenterPasswordTextBox.TabIndex = 7;
            // 
            // _dataCenterUrlTextBox
            // 
            this._dataCenterUrlTextBox.Location = new System.Drawing.Point(147, 302);
            this._dataCenterUrlTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._dataCenterUrlTextBox.Name = "_dataCenterUrlTextBox";
            this._dataCenterUrlTextBox.Size = new System.Drawing.Size(705, 21);
            this._dataCenterUrlTextBox.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(100, 332);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 15);
            this.label9.TabIndex = 44;
            this.label9.Text = "User :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(72, 363);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 15);
            this.label10.TabIndex = 43;
            this.label10.Text = "Password :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(36, 416);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 15);
            this.label11.TabIndex = 42;
            this.label11.Text = "Database Name :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(42, 303);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 15);
            this.label12.TabIndex = 41;
            this.label12.Text = "Data Center Url :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(142, 277);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 15);
            this.label13.TabIndex = 40;
            this.label13.Text = "Data Center";
            // 
            // _smlConnectButton
            // 
            this._smlConnectButton.Location = new System.Drawing.Point(879, 192);
            this._smlConnectButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._smlConnectButton.Name = "_smlConnectButton";
            this._smlConnectButton.Size = new System.Drawing.Size(84, 25);
            this._smlConnectButton.TabIndex = 4;
            this._smlConnectButton.Text = "เชื่อมต่อ";
            this._smlConnectButton.UseVisualStyleBackColor = true;
            this._smlConnectButton.Click += new System.EventHandler(this._smlConnectButton_Click);
            // 
            // _smlUserTextBox
            // 
            this._smlUserTextBox.Location = new System.Drawing.Point(147, 101);
            this._smlUserTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._smlUserTextBox.Name = "_smlUserTextBox";
            this._smlUserTextBox.Size = new System.Drawing.Size(705, 21);
            this._smlUserTextBox.TabIndex = 1;
            // 
            // _smlDatabaseNameTextBox
            // 
            this._smlDatabaseNameTextBox.Location = new System.Drawing.Point(147, 161);
            this._smlDatabaseNameTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._smlDatabaseNameTextBox.Name = "_smlDatabaseNameTextBox";
            this._smlDatabaseNameTextBox.Size = new System.Drawing.Size(705, 21);
            this._smlDatabaseNameTextBox.TabIndex = 3;
            // 
            // _smlPasswordTextBox
            // 
            this._smlPasswordTextBox.Location = new System.Drawing.Point(147, 133);
            this._smlPasswordTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._smlPasswordTextBox.Name = "_smlPasswordTextBox";
            this._smlPasswordTextBox.Size = new System.Drawing.Size(705, 21);
            this._smlPasswordTextBox.TabIndex = 2;
            // 
            // _smlServerTextBox
            // 
            this._smlServerTextBox.Location = new System.Drawing.Point(147, 73);
            this._smlServerTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._smlServerTextBox.Name = "_smlServerTextBox";
            this._smlServerTextBox.Size = new System.Drawing.Size(705, 21);
            this._smlServerTextBox.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(100, 107);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 15);
            this.label8.TabIndex = 33;
            this.label8.Text = "User :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(72, 136);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 32;
            this.label7.Text = "Password :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 167);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 15);
            this.label6.TabIndex = 29;
            this.label6.Text = "Database Name :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(91, 77);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 15);
            this.label5.TabIndex = 27;
            this.label5.Text = "Server :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(142, 39);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 15);
            this.label4.TabIndex = 25;
            this.label4.Text = "SML";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 676);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this._dataPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this._connectPage.ResumeLayout(false);
            this._connectPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel _postgreSqlConnectStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel _dataCenterConnectStatus;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage _dataPage;
        private System.Windows.Forms.TabPage _connectPage;
        private System.Windows.Forms.TextBox _dataCenterConnectStatusTextBox;
        private System.Windows.Forms.TextBox _smlConnectStatusTextBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button _dataCenterConnectButton;
        private System.Windows.Forms.TextBox _dataCenterUserTextBox;
        private System.Windows.Forms.TextBox _dataCenterDatabaseNameTextBox;
        private System.Windows.Forms.TextBox _dataCenterPasswordTextBox;
        private System.Windows.Forms.TextBox _dataCenterUrlTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button _smlConnectButton;
        private System.Windows.Forms.TextBox _smlUserTextBox;
        private System.Windows.Forms.TextBox _smlDatabaseNameTextBox;
        private System.Windows.Forms.TextBox _smlPasswordTextBox;
        private System.Windows.Forms.TextBox _smlServerTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _compareButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _dataCenterBranchCodeTextBox;
        private System.Windows.Forms.DataGridView _dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn _barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn _itemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn _itemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn _unitCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn _unitName;
        private System.Windows.Forms.DataGridViewTextBoxColumn _priceOld;
        private System.Windows.Forms.DataGridViewTextBoxColumn _priceNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton _updatePriceButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _dataCenterProviderTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ProgressBar _progressBar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _searchText;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label _processLabel;
    }
}

