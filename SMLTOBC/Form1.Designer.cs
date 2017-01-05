namespace SMLTOBC
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this._autoTabPage = new System.Windows.Forms.TabPage();
            this._autoTextBox = new System.Windows.Forms.RichTextBox();
            this._commandPage = new System.Windows.Forms.TabPage();
            this._itemButton = new System.Windows.Forms.Button();
            this._apSupplierButton = new System.Windows.Forms.Button();
            this._arCustomerButton = new System.Windows.Forms.Button();
            this._createTriggerButton = new System.Windows.Forms.Button();
            this._connectDatabasePage = new System.Windows.Forms.TabPage();
            this._bcConnectStatusTextBox = new System.Windows.Forms.TextBox();
            this._smlConnectStatusTextBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this._bcConnectButton = new System.Windows.Forms.Button();
            this._bcUserTextBox = new System.Windows.Forms.TextBox();
            this._bcDatabaseNameTextBox = new System.Windows.Forms.TextBox();
            this._bcPasswordTextBox = new System.Windows.Forms.TextBox();
            this._bcServerTextBox = new System.Windows.Forms.TextBox();
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._postgreSqlConnectStatus = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._microsoftSqlConnectStatus = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._countChapGuidLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._autoButton = new System.Windows.Forms.ToolStripButton();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this._autoTabPage.SuspendLayout();
            this._commandPage.SuspendLayout();
            this._connectDatabasePage.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this._autoTabPage);
            this.tabControl1.Controls.Add(this._commandPage);
            this.tabControl1.Controls.Add(this._connectDatabasePage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(997, 512);
            this.tabControl1.TabIndex = 0;
            // 
            // _autoTabPage
            // 
            this._autoTabPage.Controls.Add(this._autoTextBox);
            this._autoTabPage.Location = new System.Drawing.Point(4, 33);
            this._autoTabPage.Name = "_autoTabPage";
            this._autoTabPage.Size = new System.Drawing.Size(989, 475);
            this._autoTabPage.TabIndex = 2;
            this._autoTabPage.Text = "Auto";
            this._autoTabPage.UseVisualStyleBackColor = true;
            // 
            // _autoTextBox
            // 
            this._autoTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._autoTextBox.Location = new System.Drawing.Point(0, 0);
            this._autoTextBox.Name = "_autoTextBox";
            this._autoTextBox.Size = new System.Drawing.Size(989, 475);
            this._autoTextBox.TabIndex = 10;
            this._autoTextBox.Text = "";
            // 
            // _commandPage
            // 
            this._commandPage.Controls.Add(this._itemButton);
            this._commandPage.Controls.Add(this._apSupplierButton);
            this._commandPage.Controls.Add(this._arCustomerButton);
            this._commandPage.Controls.Add(this._createTriggerButton);
            this._commandPage.Location = new System.Drawing.Point(4, 33);
            this._commandPage.Name = "_commandPage";
            this._commandPage.Padding = new System.Windows.Forms.Padding(3);
            this._commandPage.Size = new System.Drawing.Size(989, 475);
            this._commandPage.TabIndex = 0;
            this._commandPage.Text = "คำสั่งพิเศษ";
            this._commandPage.UseVisualStyleBackColor = true;
            // 
            // _itemButton
            // 
            this._itemButton.Location = new System.Drawing.Point(310, 134);
            this._itemButton.Name = "_itemButton";
            this._itemButton.Size = new System.Drawing.Size(242, 32);
            this._itemButton.TabIndex = 3;
            this._itemButton.Text = "สั่งโอนสินค้าทั้งหมด (bcitem)";
            this._itemButton.UseVisualStyleBackColor = true;
            this._itemButton.Click += new System.EventHandler(this._itemButton_Click);
            // 
            // _apSupplierButton
            // 
            this._apSupplierButton.Location = new System.Drawing.Point(310, 79);
            this._apSupplierButton.Name = "_apSupplierButton";
            this._apSupplierButton.Size = new System.Drawing.Size(242, 32);
            this._apSupplierButton.TabIndex = 2;
            this._apSupplierButton.Text = "สั่งโอนผู้จำหน่ายทั้งหมด (ap_supplier)";
            this._apSupplierButton.UseVisualStyleBackColor = true;
            this._apSupplierButton.Click += new System.EventHandler(this._apSupplierButton_Click);
            // 
            // _arCustomerButton
            // 
            this._arCustomerButton.Location = new System.Drawing.Point(310, 26);
            this._arCustomerButton.Name = "_arCustomerButton";
            this._arCustomerButton.Size = new System.Drawing.Size(242, 32);
            this._arCustomerButton.TabIndex = 1;
            this._arCustomerButton.Text = "สั่งโอนลูกค้าทั้งหมด (ar_customer)";
            this._arCustomerButton.UseVisualStyleBackColor = true;
            this._arCustomerButton.Click += new System.EventHandler(this._arCustomerButton_Click);
            // 
            // _createTriggerButton
            // 
            this._createTriggerButton.Location = new System.Drawing.Point(36, 26);
            this._createTriggerButton.Name = "_createTriggerButton";
            this._createTriggerButton.Size = new System.Drawing.Size(141, 32);
            this._createTriggerButton.TabIndex = 0;
            this._createTriggerButton.Text = "สร้าง Trigger";
            this._createTriggerButton.UseVisualStyleBackColor = true;
            this._createTriggerButton.Click += new System.EventHandler(this._createTriggerButton_Click);
            // 
            // _connectDatabasePage
            // 
            this._connectDatabasePage.Controls.Add(this._bcConnectStatusTextBox);
            this._connectDatabasePage.Controls.Add(this._smlConnectStatusTextBox);
            this._connectDatabasePage.Controls.Add(this.label15);
            this._connectDatabasePage.Controls.Add(this.label14);
            this._connectDatabasePage.Controls.Add(this._bcConnectButton);
            this._connectDatabasePage.Controls.Add(this._bcUserTextBox);
            this._connectDatabasePage.Controls.Add(this._bcDatabaseNameTextBox);
            this._connectDatabasePage.Controls.Add(this._bcPasswordTextBox);
            this._connectDatabasePage.Controls.Add(this._bcServerTextBox);
            this._connectDatabasePage.Controls.Add(this.label9);
            this._connectDatabasePage.Controls.Add(this.label10);
            this._connectDatabasePage.Controls.Add(this.label11);
            this._connectDatabasePage.Controls.Add(this.label12);
            this._connectDatabasePage.Controls.Add(this.label13);
            this._connectDatabasePage.Controls.Add(this._smlConnectButton);
            this._connectDatabasePage.Controls.Add(this._smlUserTextBox);
            this._connectDatabasePage.Controls.Add(this._smlDatabaseNameTextBox);
            this._connectDatabasePage.Controls.Add(this._smlPasswordTextBox);
            this._connectDatabasePage.Controls.Add(this._smlServerTextBox);
            this._connectDatabasePage.Controls.Add(this.label8);
            this._connectDatabasePage.Controls.Add(this.label7);
            this._connectDatabasePage.Controls.Add(this.label6);
            this._connectDatabasePage.Controls.Add(this.label5);
            this._connectDatabasePage.Controls.Add(this.label4);
            this._connectDatabasePage.Location = new System.Drawing.Point(4, 33);
            this._connectDatabasePage.Name = "_connectDatabasePage";
            this._connectDatabasePage.Padding = new System.Windows.Forms.Padding(3);
            this._connectDatabasePage.Size = new System.Drawing.Size(989, 475);
            this._connectDatabasePage.TabIndex = 1;
            this._connectDatabasePage.Text = "เชื่อมต่อ";
            this._connectDatabasePage.UseVisualStyleBackColor = true;
            // 
            // _bcConnectStatusTextBox
            // 
            this._bcConnectStatusTextBox.Location = new System.Drawing.Point(193, 380);
            this._bcConnectStatusTextBox.Name = "_bcConnectStatusTextBox";
            this._bcConnectStatusTextBox.ReadOnly = true;
            this._bcConnectStatusTextBox.Size = new System.Drawing.Size(354, 31);
            this._bcConnectStatusTextBox.TabIndex = 24;
            // 
            // _smlConnectStatusTextBox
            // 
            this._smlConnectStatusTextBox.Location = new System.Drawing.Point(193, 175);
            this._smlConnectStatusTextBox.Name = "_smlConnectStatusTextBox";
            this._smlConnectStatusTextBox.ReadOnly = true;
            this._smlConnectStatusTextBox.Size = new System.Drawing.Size(354, 31);
            this._smlConnectStatusTextBox.TabIndex = 23;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(70, 387);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(177, 24);
            this.label15.TabIndex = 22;
            this.label15.Text = "สถานะการเชื่อมต่อ :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(70, 177);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(177, 24);
            this.label14.TabIndex = 21;
            this.label14.Text = "สถานะการเชื่อมต่อ :";
            // 
            // _bcConnectButton
            // 
            this._bcConnectButton.Location = new System.Drawing.Point(553, 353);
            this._bcConnectButton.Name = "_bcConnectButton";
            this._bcConnectButton.Size = new System.Drawing.Size(75, 23);
            this._bcConnectButton.TabIndex = 9;
            this._bcConnectButton.Text = "เชื่อมต่อ";
            this._bcConnectButton.UseVisualStyleBackColor = true;
            this._bcConnectButton.Click += new System.EventHandler(this._bcConnectButton_Click);
            // 
            // _bcUserTextBox
            // 
            this._bcUserTextBox.Location = new System.Drawing.Point(193, 299);
            this._bcUserTextBox.Name = "_bcUserTextBox";
            this._bcUserTextBox.Size = new System.Drawing.Size(354, 31);
            this._bcUserTextBox.TabIndex = 6;
            // 
            // _bcDatabaseNameTextBox
            // 
            this._bcDatabaseNameTextBox.Location = new System.Drawing.Point(193, 353);
            this._bcDatabaseNameTextBox.Name = "_bcDatabaseNameTextBox";
            this._bcDatabaseNameTextBox.Size = new System.Drawing.Size(354, 31);
            this._bcDatabaseNameTextBox.TabIndex = 8;
            // 
            // _bcPasswordTextBox
            // 
            this._bcPasswordTextBox.Location = new System.Drawing.Point(193, 326);
            this._bcPasswordTextBox.Name = "_bcPasswordTextBox";
            this._bcPasswordTextBox.Size = new System.Drawing.Size(354, 31);
            this._bcPasswordTextBox.TabIndex = 7;
            // 
            // _bcServerTextBox
            // 
            this._bcServerTextBox.Location = new System.Drawing.Point(193, 272);
            this._bcServerTextBox.Name = "_bcServerTextBox";
            this._bcServerTextBox.Size = new System.Drawing.Size(354, 31);
            this._bcServerTextBox.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(144, 302);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 24);
            this.label9.TabIndex = 14;
            this.label9.Text = "User :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(116, 329);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 24);
            this.label10.TabIndex = 13;
            this.label10.Text = "Password :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(80, 356);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(166, 24);
            this.label11.TabIndex = 12;
            this.label11.Text = "Database Name :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(132, 275);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 24);
            this.label12.TabIndex = 11;
            this.label12.Text = "Server :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(193, 244);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 24);
            this.label13.TabIndex = 10;
            this.label13.Text = "BC";
            // 
            // _smlConnectButton
            // 
            this._smlConnectButton.Location = new System.Drawing.Point(553, 147);
            this._smlConnectButton.Name = "_smlConnectButton";
            this._smlConnectButton.Size = new System.Drawing.Size(75, 23);
            this._smlConnectButton.TabIndex = 4;
            this._smlConnectButton.Text = "เชื่อมต่อ";
            this._smlConnectButton.UseVisualStyleBackColor = true;
            this._smlConnectButton.Click += new System.EventHandler(this._smlConnectButton_Click);
            // 
            // _smlUserTextBox
            // 
            this._smlUserTextBox.Location = new System.Drawing.Point(193, 93);
            this._smlUserTextBox.Name = "_smlUserTextBox";
            this._smlUserTextBox.Size = new System.Drawing.Size(354, 31);
            this._smlUserTextBox.TabIndex = 1;
            // 
            // _smlDatabaseNameTextBox
            // 
            this._smlDatabaseNameTextBox.Location = new System.Drawing.Point(193, 147);
            this._smlDatabaseNameTextBox.Name = "_smlDatabaseNameTextBox";
            this._smlDatabaseNameTextBox.Size = new System.Drawing.Size(354, 31);
            this._smlDatabaseNameTextBox.TabIndex = 3;
            // 
            // _smlPasswordTextBox
            // 
            this._smlPasswordTextBox.Location = new System.Drawing.Point(193, 120);
            this._smlPasswordTextBox.Name = "_smlPasswordTextBox";
            this._smlPasswordTextBox.Size = new System.Drawing.Size(354, 31);
            this._smlPasswordTextBox.TabIndex = 2;
            // 
            // _smlServerTextBox
            // 
            this._smlServerTextBox.Location = new System.Drawing.Point(193, 67);
            this._smlServerTextBox.Name = "_smlServerTextBox";
            this._smlServerTextBox.Size = new System.Drawing.Size(354, 31);
            this._smlServerTextBox.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(144, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 24);
            this.label8.TabIndex = 4;
            this.label8.Text = "User :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(116, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 24);
            this.label7.TabIndex = 3;
            this.label7.Text = "Password :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(80, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(166, 24);
            this.label6.TabIndex = 2;
            this.label6.Text = "Database Name :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(132, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 24);
            this.label5.TabIndex = 1;
            this.label5.Text = "Server :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(193, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 24);
            this.label4.TabIndex = 0;
            this.label4.Text = "SML";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._postgreSqlConnectStatus,
            this.toolStripSeparator2,
            this._microsoftSqlConnectStatus,
            this.toolStripSeparator1,
            this._countChapGuidLabel,
            this.toolStripSeparator3,
            this._autoButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(997, 32);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _postgreSqlConnectStatus
            // 
            this._postgreSqlConnectStatus.Name = "_postgreSqlConnectStatus";
            this._postgreSqlConnectStatus.Size = new System.Drawing.Size(221, 29);
            this._postgreSqlConnectStatus.Text = "PostgeSQL Connect Status";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // _microsoftSqlConnectStatus
            // 
            this._microsoftSqlConnectStatus.Name = "_microsoftSqlConnectStatus";
            this._microsoftSqlConnectStatus.Size = new System.Drawing.Size(243, 29);
            this._microsoftSqlConnectStatus.Text = "MicrosoftSQL Connect Status";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // _countChapGuidLabel
            // 
            this._countChapGuidLabel.Name = "_countChapGuidLabel";
            this._countChapGuidLabel.Size = new System.Drawing.Size(24, 29);
            this._countChapGuidLabel.Text = "...";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
            // 
            // _autoButton
            // 
            this._autoButton.Image = ((System.Drawing.Image)(resources.GetObject("_autoButton.Image")));
            this._autoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._autoButton.Name = "_autoButton";
            this._autoButton.Size = new System.Drawing.Size(129, 29);
            this._autoButton.Text = "เริ่มโอนข้อมูล";
            this._autoButton.Click += new System.EventHandler(this._autoButton_Click);
            // 
            // _timer
            // 
            this._timer.Enabled = true;
            this._timer.Interval = 1000;
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 544);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this._autoTabPage.ResumeLayout(false);
            this._commandPage.ResumeLayout(false);
            this._connectDatabasePage.ResumeLayout(false);
            this._connectDatabasePage.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage _connectDatabasePage;
        private System.Windows.Forms.TabPage _commandPage;
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
        private System.Windows.Forms.Button _bcConnectButton;
        private System.Windows.Forms.TextBox _bcUserTextBox;
        private System.Windows.Forms.TextBox _bcDatabaseNameTextBox;
        private System.Windows.Forms.TextBox _bcPasswordTextBox;
        private System.Windows.Forms.TextBox _bcServerTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox _bcConnectStatusTextBox;
        private System.Windows.Forms.TextBox _smlConnectStatusTextBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel _postgreSqlConnectStatus;
        private System.Windows.Forms.ToolStripLabel _microsoftSqlConnectStatus;
        private System.Windows.Forms.TabPage _autoTabPage;
        private System.Windows.Forms.RichTextBox _autoTextBox;
        private System.Windows.Forms.Timer _timer;
        private System.Windows.Forms.Button _createTriggerButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _autoButton;
        private System.Windows.Forms.Button _arCustomerButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel _countChapGuidLabel;
        private System.Windows.Forms.Button _apSupplierButton;
        private System.Windows.Forms.Button _itemButton;
    }
}

