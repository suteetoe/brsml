namespace SMLExecuteQueryTools
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
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
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this._queryGroupBox = new System.Windows.Forms.GroupBox();
            this._resultDatagridView = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this._executeResult = new System.Windows.Forms.Label();
            this._executeButton = new System.Windows.Forms.Button();
            this._queryButton = new System.Windows.Forms.Button();
            this._textBoxQuery = new System.Windows.Forms.TextBox();
            this._testQuery = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this._queryGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._resultDatagridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._testQuery);
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
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(5, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(986, 172);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Connect Server";
            // 
            // _desConnectedStatus
            // 
            this._desConnectedStatus.AutoSize = true;
            this._desConnectedStatus.Location = new System.Drawing.Point(540, 143);
            this._desConnectedStatus.Name = "_desConnectedStatus";
            this._desConnectedStatus.Size = new System.Drawing.Size(0, 14);
            this._desConnectedStatus.TabIndex = 18;
            // 
            // _desDatabaseNameTextbox
            // 
            this._desDatabaseNameTextbox.Location = new System.Drawing.Point(184, 140);
            this._desDatabaseNameTextbox.Name = "_desDatabaseNameTextbox";
            this._desDatabaseNameTextbox.Size = new System.Drawing.Size(243, 22);
            this._desDatabaseNameTextbox.TabIndex = 11;
            // 
            // _desConnectButton
            // 
            this._desConnectButton.Location = new System.Drawing.Point(433, 140);
            this._desConnectButton.Name = "_desConnectButton";
            this._desConnectButton.Size = new System.Drawing.Size(101, 22);
            this._desConnectButton.TabIndex = 16;
            this._desConnectButton.Text = "Connect";
            this._desConnectButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._desConnectButton.UseVisualStyleBackColor = true;
            this._desConnectButton.Click += new System.EventHandler(this._desConnectButton_Click);
            // 
            // _desPasswordTextbox
            // 
            this._desPasswordTextbox.Location = new System.Drawing.Point(184, 116);
            this._desPasswordTextbox.Name = "_desPasswordTextbox";
            this._desPasswordTextbox.Size = new System.Drawing.Size(243, 22);
            this._desPasswordTextbox.TabIndex = 10;
            // 
            // _desUserCodeTextbox
            // 
            this._desUserCodeTextbox.Location = new System.Drawing.Point(184, 92);
            this._desUserCodeTextbox.Name = "_desUserCodeTextbox";
            this._desUserCodeTextbox.Size = new System.Drawing.Size(243, 22);
            this._desUserCodeTextbox.TabIndex = 9;
            // 
            // _desGroupTextbox
            // 
            this._desGroupTextbox.Location = new System.Drawing.Point(184, 68);
            this._desGroupTextbox.Name = "_desGroupTextbox";
            this._desGroupTextbox.Size = new System.Drawing.Size(243, 22);
            this._desGroupTextbox.TabIndex = 8;
            // 
            // _desProviderTextbox
            // 
            this._desProviderTextbox.Location = new System.Drawing.Point(184, 44);
            this._desProviderTextbox.Name = "_desProviderTextbox";
            this._desProviderTextbox.Size = new System.Drawing.Size(243, 22);
            this._desProviderTextbox.TabIndex = 7;
            // 
            // _desWebServiceTextbox
            // 
            this._desWebServiceTextbox.Location = new System.Drawing.Point(184, 20);
            this._desWebServiceTextbox.Name = "_desWebServiceTextbox";
            this._desWebServiceTextbox.Size = new System.Drawing.Size(243, 22);
            this._desWebServiceTextbox.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(78, 143);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 14);
            this.label7.TabIndex = 5;
            this.label7.Text = "Database Name :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(112, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 14);
            this.label8.TabIndex = 4;
            this.label8.Text = "Password :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(112, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 14);
            this.label9.TabIndex = 3;
            this.label9.Text = "Usercode :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(130, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 14);
            this.label10.TabIndex = 2;
            this.label10.Text = "Group :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(119, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 14);
            this.label11.TabIndex = 1;
            this.label11.Text = "Provider :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(69, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(109, 14);
            this.label12.TabIndex = 0;
            this.label12.Text = "Web Service URL :";
            // 
            // _queryGroupBox
            // 
            this._queryGroupBox.Controls.Add(this._resultDatagridView);
            this._queryGroupBox.Controls.Add(this.panel1);
            this._queryGroupBox.Controls.Add(this._textBoxQuery);
            this._queryGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._queryGroupBox.Location = new System.Drawing.Point(5, 177);
            this._queryGroupBox.Name = "_queryGroupBox";
            this._queryGroupBox.Size = new System.Drawing.Size(986, 571);
            this._queryGroupBox.TabIndex = 41;
            this._queryGroupBox.TabStop = false;
            this._queryGroupBox.Text = "Query";
            // 
            // _resultDatagridView
            // 
            this._resultDatagridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._resultDatagridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultDatagridView.Location = new System.Drawing.Point(3, 122);
            this._resultDatagridView.Name = "_resultDatagridView";
            this._resultDatagridView.Size = new System.Drawing.Size(980, 446);
            this._resultDatagridView.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._executeResult);
            this.panel1.Controls.Add(this._executeButton);
            this.panel1.Controls.Add(this._queryButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 91);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(980, 31);
            this.panel1.TabIndex = 13;
            // 
            // _executeResult
            // 
            this._executeResult.Location = new System.Drawing.Point(3, 3);
            this._executeResult.Name = "_executeResult";
            this._executeResult.Size = new System.Drawing.Size(779, 23);
            this._executeResult.TabIndex = 19;
            this._executeResult.Text = "Database Name :";
            // 
            // _executeButton
            // 
            this._executeButton.Location = new System.Drawing.Point(881, 3);
            this._executeButton.Name = "_executeButton";
            this._executeButton.Size = new System.Drawing.Size(96, 23);
            this._executeButton.TabIndex = 18;
            this._executeButton.Text = "Execute";
            this._executeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._executeButton.UseVisualStyleBackColor = true;
            this._executeButton.Click += new System.EventHandler(this._executeButton_Click);
            // 
            // _queryButton
            // 
            this._queryButton.Location = new System.Drawing.Point(788, 3);
            this._queryButton.Name = "_queryButton";
            this._queryButton.Size = new System.Drawing.Size(87, 23);
            this._queryButton.TabIndex = 17;
            this._queryButton.Text = "Query";
            this._queryButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._queryButton.UseVisualStyleBackColor = true;
            this._queryButton.Click += new System.EventHandler(this._queryButton_Click);
            // 
            // _textBoxQuery
            // 
            this._textBoxQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this._textBoxQuery.Location = new System.Drawing.Point(3, 18);
            this._textBoxQuery.Multiline = true;
            this._textBoxQuery.Name = "_textBoxQuery";
            this._textBoxQuery.Size = new System.Drawing.Size(980, 73);
            this._textBoxQuery.TabIndex = 12;
            // 
            // _testQuery
            // 
            this._testQuery.Location = new System.Drawing.Point(545, 140);
            this._testQuery.Name = "_testQuery";
            this._testQuery.Size = new System.Drawing.Size(101, 22);
            this._testQuery.TabIndex = 19;
            this._testQuery.Text = "Test Query";
            this._testQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._testQuery.UseVisualStyleBackColor = true;
            this._testQuery.Click += new System.EventHandler(this._testQuery_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 753);
            this.Controls.Add(this._queryGroupBox);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "SML Execute Query Tools";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this._queryGroupBox.ResumeLayout(false);
            this._queryGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._resultDatagridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
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
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox _queryGroupBox;
        private System.Windows.Forms.TextBox _textBoxQuery;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button _executeButton;
        private System.Windows.Forms.Button _queryButton;
        private System.Windows.Forms.Label _executeResult;
        private System.Windows.Forms.DataGridView _resultDatagridView;
        private System.Windows.Forms.Button _testQuery;
    }
}

