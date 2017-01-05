namespace SMLSyncCheckingTools
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
            this._tableName = new System.Windows.Forms.TextBox();
            this._indentityFieldName = new System.Windows.Forms.TextBox();
            this._groupCondition = new System.Windows.Forms.GroupBox();
            this._keyGrid = new System.Windows.Forms.DataGridView();
            this._fieldDataGridViewFieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._fieldTypeColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this._startButton = new System.Windows.Forms.Button();
            this._desConnectedStatus = new System.Windows.Forms.Label();
            this._desDatabaseNameTextbox = new System.Windows.Forms.TextBox();
            this._desConnectButton = new System.Windows.Forms.Button();
            this._desPasswordTextbox = new System.Windows.Forms.TextBox();
            this._desUserCodeTextbox = new System.Windows.Forms.TextBox();
            this._desGroupTextbox = new System.Windows.Forms.TextBox();
            this._desProviderTextbox = new System.Windows.Forms.TextBox();
            this._desWebServiceTextbox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this._sourceConnectdStatus = new System.Windows.Forms.Label();
            this._sourceDatabaseNameTextbox = new System.Windows.Forms.TextBox();
            this._sourceConnectButton = new System.Windows.Forms.Button();
            this._sourcePasswordTextbox = new System.Windows.Forms.TextBox();
            this._sourceUserCodeTextbox = new System.Windows.Forms.TextBox();
            this._sourceGroupTextbox = new System.Windows.Forms.TextBox();
            this._sourceProviderTextbox = new System.Windows.Forms.TextBox();
            this._sourceWebServiceTextbox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._stopButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this._genUpdateGuid = new System.Windows.Forms.Button();
            this._processLabel = new System.Windows.Forms.Label();
            this._resultGrid = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this._labelStatusInfo = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this._groupCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._keyGrid)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._resultGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // _tableName
            // 
            this._tableName.Location = new System.Drawing.Point(126, 43);
            this._tableName.Name = "_tableName";
            this._tableName.Size = new System.Drawing.Size(209, 22);
            this._tableName.TabIndex = 23;
            // 
            // _indentityFieldName
            // 
            this._indentityFieldName.Location = new System.Drawing.Point(126, 18);
            this._indentityFieldName.Name = "_indentityFieldName";
            this._indentityFieldName.Size = new System.Drawing.Size(209, 22);
            this._indentityFieldName.TabIndex = 21;
            this._indentityFieldName.Text = "guid";
            // 
            // _groupCondition
            // 
            this._groupCondition.Controls.Add(this.button1);
            this._groupCondition.Controls.Add(this._keyGrid);
            this._groupCondition.Controls.Add(this._tableName);
            this._groupCondition.Controls.Add(this._indentityFieldName);
            this._groupCondition.Controls.Add(this.label14);
            this._groupCondition.Controls.Add(this.label13);
            this._groupCondition.Location = new System.Drawing.Point(14, 229);
            this._groupCondition.Name = "_groupCondition";
            this._groupCondition.Size = new System.Drawing.Size(500, 502);
            this._groupCondition.TabIndex = 41;
            this._groupCondition.TabStop = false;
            this._groupCondition.Text = "Table, Key Compare";
            // 
            // _keyGrid
            // 
            this._keyGrid.AllowUserToResizeRows = false;
            this._keyGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._keyGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._fieldDataGridViewFieldName,
            this._fieldTypeColumn,
            this.line});
            this._keyGrid.Location = new System.Drawing.Point(6, 71);
            this._keyGrid.MultiSelect = false;
            this._keyGrid.Name = "_keyGrid";
            this._keyGrid.RowHeadersVisible = false;
            this._keyGrid.Size = new System.Drawing.Size(488, 425);
            this._keyGrid.TabIndex = 24;
            // 
            // _fieldDataGridViewFieldName
            // 
            this._fieldDataGridViewFieldName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._fieldDataGridViewFieldName.HeaderText = "Field Name";
            this._fieldDataGridViewFieldName.Name = "_fieldDataGridViewFieldName";
            // 
            // _fieldTypeColumn
            // 
            this._fieldTypeColumn.HeaderText = "Type";
            this._fieldTypeColumn.Items.AddRange(new object[] {
            "String",
            "Number",
            "Date"});
            this._fieldTypeColumn.Name = "_fieldTypeColumn";
            this._fieldTypeColumn.Width = 140;
            // 
            // line
            // 
            this.line.HeaderText = "line";
            this.line.Name = "line";
            this.line.ReadOnly = true;
            this.line.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(35, 47);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 14);
            this.label14.TabIndex = 18;
            this.label14.Text = "Target Table :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(27, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 14);
            this.label13.TabIndex = 16;
            this.label13.Text = "Indentity Field :";
            // 
            // _startButton
            // 
            this._startButton.Location = new System.Drawing.Point(840, 737);
            this._startButton.Name = "_startButton";
            this._startButton.Size = new System.Drawing.Size(87, 25);
            this._startButton.TabIndex = 43;
            this._startButton.Text = "Start";
            this._startButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._startButton.UseVisualStyleBackColor = true;
            this._startButton.Click += new System.EventHandler(this._startButton_Click);
            // 
            // _desConnectedStatus
            // 
            this._desConnectedStatus.AutoSize = true;
            this._desConnectedStatus.Location = new System.Drawing.Point(296, 176);
            this._desConnectedStatus.Name = "_desConnectedStatus";
            this._desConnectedStatus.Size = new System.Drawing.Size(0, 14);
            this._desConnectedStatus.TabIndex = 18;
            // 
            // _desDatabaseNameTextbox
            // 
            this._desDatabaseNameTextbox.Location = new System.Drawing.Point(158, 146);
            this._desDatabaseNameTextbox.Name = "_desDatabaseNameTextbox";
            this._desDatabaseNameTextbox.Size = new System.Drawing.Size(209, 22);
            this._desDatabaseNameTextbox.TabIndex = 11;
            // 
            // _desConnectButton
            // 
            this._desConnectButton.Location = new System.Drawing.Point(158, 172);
            this._desConnectButton.Name = "_desConnectButton";
            this._desConnectButton.Size = new System.Drawing.Size(131, 25);
            this._desConnectButton.TabIndex = 16;
            this._desConnectButton.Text = "Connect";
            this._desConnectButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._desConnectButton.UseVisualStyleBackColor = true;
            this._desConnectButton.Click += new System.EventHandler(this._desConnectButton_Click);
            // 
            // _desPasswordTextbox
            // 
            this._desPasswordTextbox.Location = new System.Drawing.Point(158, 120);
            this._desPasswordTextbox.Name = "_desPasswordTextbox";
            this._desPasswordTextbox.Size = new System.Drawing.Size(209, 22);
            this._desPasswordTextbox.TabIndex = 10;
            // 
            // _desUserCodeTextbox
            // 
            this._desUserCodeTextbox.Location = new System.Drawing.Point(158, 96);
            this._desUserCodeTextbox.Name = "_desUserCodeTextbox";
            this._desUserCodeTextbox.Size = new System.Drawing.Size(209, 22);
            this._desUserCodeTextbox.TabIndex = 9;
            this._desUserCodeTextbox.Text = "superadmin";
            // 
            // _desGroupTextbox
            // 
            this._desGroupTextbox.Location = new System.Drawing.Point(158, 70);
            this._desGroupTextbox.Name = "_desGroupTextbox";
            this._desGroupTextbox.Size = new System.Drawing.Size(209, 22);
            this._desGroupTextbox.TabIndex = 8;
            this._desGroupTextbox.Text = "SML";
            // 
            // _desProviderTextbox
            // 
            this._desProviderTextbox.Location = new System.Drawing.Point(158, 45);
            this._desProviderTextbox.Name = "_desProviderTextbox";
            this._desProviderTextbox.Size = new System.Drawing.Size(209, 22);
            this._desProviderTextbox.TabIndex = 7;
            this._desProviderTextbox.Text = "DATA";
            // 
            // _desWebServiceTextbox
            // 
            this._desWebServiceTextbox.Location = new System.Drawing.Point(158, 19);
            this._desWebServiceTextbox.Name = "_desWebServiceTextbox";
            this._desWebServiceTextbox.Size = new System.Drawing.Size(209, 22);
            this._desWebServiceTextbox.TabIndex = 6;
            this._desWebServiceTextbox.Text = "192.168.64.49:8008";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 14);
            this.label7.TabIndex = 5;
            this.label7.Text = "Database Name :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(76, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 14);
            this.label8.TabIndex = 4;
            this.label8.Text = "Password :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(78, 99);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 14);
            this.label9.TabIndex = 3;
            this.label9.Text = "Usercode :";
            // 
            // _sourceConnectdStatus
            // 
            this._sourceConnectdStatus.AutoSize = true;
            this._sourceConnectdStatus.Location = new System.Drawing.Point(323, 176);
            this._sourceConnectdStatus.Name = "_sourceConnectdStatus";
            this._sourceConnectdStatus.Size = new System.Drawing.Size(0, 14);
            this._sourceConnectdStatus.TabIndex = 17;
            // 
            // _sourceDatabaseNameTextbox
            // 
            this._sourceDatabaseNameTextbox.Location = new System.Drawing.Point(185, 145);
            this._sourceDatabaseNameTextbox.Name = "_sourceDatabaseNameTextbox";
            this._sourceDatabaseNameTextbox.Size = new System.Drawing.Size(209, 22);
            this._sourceDatabaseNameTextbox.TabIndex = 11;
            // 
            // _sourceConnectButton
            // 
            this._sourceConnectButton.Location = new System.Drawing.Point(185, 171);
            this._sourceConnectButton.Name = "_sourceConnectButton";
            this._sourceConnectButton.Size = new System.Drawing.Size(131, 25);
            this._sourceConnectButton.TabIndex = 15;
            this._sourceConnectButton.Text = "Connect";
            this._sourceConnectButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._sourceConnectButton.UseVisualStyleBackColor = true;
            this._sourceConnectButton.Click += new System.EventHandler(this._sourceConnectButton_Click);
            // 
            // _sourcePasswordTextbox
            // 
            this._sourcePasswordTextbox.Location = new System.Drawing.Point(185, 120);
            this._sourcePasswordTextbox.Name = "_sourcePasswordTextbox";
            this._sourcePasswordTextbox.PasswordChar = '*';
            this._sourcePasswordTextbox.Size = new System.Drawing.Size(209, 22);
            this._sourcePasswordTextbox.TabIndex = 10;
            // 
            // _sourceUserCodeTextbox
            // 
            this._sourceUserCodeTextbox.Location = new System.Drawing.Point(185, 95);
            this._sourceUserCodeTextbox.Name = "_sourceUserCodeTextbox";
            this._sourceUserCodeTextbox.Size = new System.Drawing.Size(209, 22);
            this._sourceUserCodeTextbox.TabIndex = 9;
            this._sourceUserCodeTextbox.Text = "superadmin";
            // 
            // _sourceGroupTextbox
            // 
            this._sourceGroupTextbox.Location = new System.Drawing.Point(185, 69);
            this._sourceGroupTextbox.Name = "_sourceGroupTextbox";
            this._sourceGroupTextbox.Size = new System.Drawing.Size(209, 22);
            this._sourceGroupTextbox.TabIndex = 8;
            this._sourceGroupTextbox.Text = "SML";
            // 
            // _sourceProviderTextbox
            // 
            this._sourceProviderTextbox.Location = new System.Drawing.Point(185, 43);
            this._sourceProviderTextbox.Name = "_sourceProviderTextbox";
            this._sourceProviderTextbox.Size = new System.Drawing.Size(209, 22);
            this._sourceProviderTextbox.TabIndex = 7;
            this._sourceProviderTextbox.Text = "DATACENTER";
            // 
            // _sourceWebServiceTextbox
            // 
            this._sourceWebServiceTextbox.Location = new System.Drawing.Point(185, 18);
            this._sourceWebServiceTextbox.Name = "_sourceWebServiceTextbox";
            this._sourceWebServiceTextbox.Size = new System.Drawing.Size(209, 22);
            this._sourceWebServiceTextbox.TabIndex = 6;
            this._sourceWebServiceTextbox.Text = "192.168.64.48:8008";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(66, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 14);
            this.label6.TabIndex = 5;
            this.label6.Text = "Database Name :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(103, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "Password :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(105, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "Usercode :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(124, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "Group :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Provider :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Web Service URL :";
            // 
            // _stopButton
            // 
            this._stopButton.Location = new System.Drawing.Point(933, 737);
            this._stopButton.Name = "_stopButton";
            this._stopButton.Size = new System.Drawing.Size(87, 25);
            this._stopButton.TabIndex = 44;
            this._stopButton.Text = "Stop";
            this._stopButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._stopButton.UseVisualStyleBackColor = true;
            this._stopButton.Click += new System.EventHandler(this._stopButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._desConnectedStatus);
            this.groupBox2.Controls.Add(this._desDatabaseNameTextbox);
            this.groupBox2.Controls.Add(this._desConnectButton);
            this.groupBox2.Controls.Add(this._desPasswordTextbox);
            this.groupBox2.Controls.Add(this._desUserCodeTextbox);
            this.groupBox2.Controls.Add(this._desGroupTextbox);
            this.groupBox2.Controls.Add(this._desProviderTextbox);
            this.groupBox2.Controls.Add(this._desWebServiceTextbox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Location = new System.Drawing.Point(520, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(500, 210);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Destination (ปลายทาง)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(97, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 14);
            this.label10.TabIndex = 2;
            this.label10.Text = "Group :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(84, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 14);
            this.label11.TabIndex = 1;
            this.label11.Text = "Provider :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(28, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(109, 14);
            this.label12.TabIndex = 0;
            this.label12.Text = "Web Service URL :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._sourceConnectdStatus);
            this.groupBox1.Controls.Add(this._sourceDatabaseNameTextbox);
            this.groupBox1.Controls.Add(this._sourceConnectButton);
            this.groupBox1.Controls.Add(this._sourcePasswordTextbox);
            this.groupBox1.Controls.Add(this._sourceUserCodeTextbox);
            this.groupBox1.Controls.Add(this._sourceGroupTextbox);
            this.groupBox1.Controls.Add(this._sourceProviderTextbox);
            this.groupBox1.Controls.Add(this._sourceWebServiceTextbox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 210);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sync Center Server (ต้นทาง)";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this._genUpdateGuid);
            this.groupBox4.Controls.Add(this._processLabel);
            this.groupBox4.Controls.Add(this._resultGrid);
            this.groupBox4.Location = new System.Drawing.Point(523, 229);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(500, 502);
            this.groupBox4.TabIndex = 45;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ผลการตรวจสอบ";
            // 
            // _genUpdateGuid
            // 
            this._genUpdateGuid.Location = new System.Drawing.Point(332, 471);
            this._genUpdateGuid.Name = "_genUpdateGuid";
            this._genUpdateGuid.Size = new System.Drawing.Size(162, 25);
            this._genUpdateGuid.TabIndex = 47;
            this._genUpdateGuid.Text = "Gen Update Guid Server";
            this._genUpdateGuid.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._genUpdateGuid.UseVisualStyleBackColor = true;
            this._genUpdateGuid.Click += new System.EventHandler(this._genUpdateGuid_Click);
            // 
            // _processLabel
            // 
            this._processLabel.Location = new System.Drawing.Point(6, 18);
            this._processLabel.Name = "_processLabel";
            this._processLabel.Size = new System.Drawing.Size(283, 21);
            this._processLabel.TabIndex = 25;
            this._processLabel.Text = "Indentity Field :";
            // 
            // _resultGrid
            // 
            this._resultGrid.AllowUserToAddRows = false;
            this._resultGrid.AllowUserToDeleteRows = false;
            this._resultGrid.AllowUserToResizeRows = false;
            this._resultGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._resultGrid.Location = new System.Drawing.Point(6, 43);
            this._resultGrid.Name = "_resultGrid";
            this._resultGrid.RowHeadersVisible = false;
            this._resultGrid.Size = new System.Drawing.Size(488, 422);
            this._resultGrid.TabIndex = 24;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // _labelStatusInfo
            // 
            this._labelStatusInfo.Location = new System.Drawing.Point(17, 734);
            this._labelStatusInfo.Name = "_labelStatusInfo";
            this._labelStatusInfo.Size = new System.Drawing.Size(817, 29);
            this._labelStatusInfo.TabIndex = 46;
            this._labelStatusInfo.Text = "Indentity Field :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(341, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 25);
            this.button1.TabIndex = 47;
            this.button1.Text = "Start";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 774);
            this.Controls.Add(this._labelStatusInfo);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this._groupCondition);
            this.Controls.Add(this._startButton);
            this.Controls.Add(this._stopButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "Form1";
            this.Text = "SML Syncronize Checking Tools";
            this._groupCondition.ResumeLayout(false);
            this._groupCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._keyGrid)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._resultGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox _tableName;
        private System.Windows.Forms.TextBox _indentityFieldName;
        private System.Windows.Forms.GroupBox _groupCondition;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button _startButton;
        private System.Windows.Forms.Label _desConnectedStatus;
        private System.Windows.Forms.TextBox _desDatabaseNameTextbox;
        private System.Windows.Forms.Button _desConnectButton;
        private System.Windows.Forms.TextBox _desPasswordTextbox;
        private System.Windows.Forms.TextBox _desUserCodeTextbox;
        private System.Windows.Forms.TextBox _desGroupTextbox;
        private System.Windows.Forms.TextBox _desProviderTextbox;
        private System.Windows.Forms.TextBox _desWebServiceTextbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label _sourceConnectdStatus;
        private System.Windows.Forms.TextBox _sourceDatabaseNameTextbox;
        private System.Windows.Forms.Button _sourceConnectButton;
        private System.Windows.Forms.TextBox _sourcePasswordTextbox;
        private System.Windows.Forms.TextBox _sourceUserCodeTextbox;
        private System.Windows.Forms.TextBox _sourceGroupTextbox;
        private System.Windows.Forms.TextBox _sourceProviderTextbox;
        private System.Windows.Forms.TextBox _sourceWebServiceTextbox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _stopButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView _keyGrid;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView _resultGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn _fieldDataGridViewFieldName;
        private System.Windows.Forms.DataGridViewComboBoxColumn _fieldTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn line;
        private System.Windows.Forms.Label _processLabel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label _labelStatusInfo;
        private System.Windows.Forms.Button _genUpdateGuid;
        private System.Windows.Forms.Button button1;
    }
}

