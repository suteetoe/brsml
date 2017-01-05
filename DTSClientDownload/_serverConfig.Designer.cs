namespace DTSClientDownload
{
    partial class _serverConfig
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
            this._saveButton = new System.Windows.Forms.Button();
            this._userPasswordTextBox = new System.Windows.Forms.TextBox();
            this._userCodeTextBox = new System.Windows.Forms.TextBox();
            this._databaseNameTextBox = new System.Windows.Forms.TextBox();
            this._serverNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._connectButton = new System.Windows.Forms.Button();
            this._closeButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._serverIpAddress = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this._agentPasswordTextbox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this._agentcodeTextbox = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this._resultLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _saveButton
            // 
            this._saveButton.Location = new System.Drawing.Point(260, 254);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(75, 23);
            this._saveButton.TabIndex = 17;
            this._saveButton.Text = "Save";
            this._saveButton.UseVisualStyleBackColor = true;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _userPasswordTextBox
            // 
            this._userPasswordTextBox.Location = new System.Drawing.Point(119, 98);
            this._userPasswordTextBox.Name = "_userPasswordTextBox";
            this._userPasswordTextBox.PasswordChar = '*';
            this._userPasswordTextBox.Size = new System.Drawing.Size(184, 20);
            this._userPasswordTextBox.TabIndex = 16;
            // 
            // _userCodeTextBox
            // 
            this._userCodeTextBox.Location = new System.Drawing.Point(119, 72);
            this._userCodeTextBox.Name = "_userCodeTextBox";
            this._userCodeTextBox.Size = new System.Drawing.Size(184, 20);
            this._userCodeTextBox.TabIndex = 15;
            // 
            // _databaseNameTextBox
            // 
            this._databaseNameTextBox.Location = new System.Drawing.Point(119, 45);
            this._databaseNameTextBox.Name = "_databaseNameTextBox";
            this._databaseNameTextBox.Size = new System.Drawing.Size(184, 20);
            this._databaseNameTextBox.TabIndex = 14;
            // 
            // _serverNameTextBox
            // 
            this._serverNameTextBox.Location = new System.Drawing.Point(119, 19);
            this._serverNameTextBox.Name = "_serverNameTextBox";
            this._serverNameTextBox.Size = new System.Drawing.Size(184, 20);
            this._serverNameTextBox.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(55, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Password :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "User Code :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Database Name :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Server Name :";
            // 
            // _connectButton
            // 
            this._connectButton.Location = new System.Drawing.Point(152, 254);
            this._connectButton.Name = "_connectButton";
            this._connectButton.Size = new System.Drawing.Size(102, 23);
            this._connectButton.TabIndex = 18;
            this._connectButton.Text = "Test Connect";
            this._connectButton.UseVisualStyleBackColor = true;
            this._connectButton.Visible = false;
            this._connectButton.Click += new System.EventHandler(this._connectButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Location = new System.Drawing.Point(121, 254);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(75, 23);
            this._closeButton.TabIndex = 19;
            this._closeButton.Text = "Close";
            this._closeButton.UseVisualStyleBackColor = true;
            this._closeButton.Visible = false;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._serverNameTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this._userPasswordTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this._userCodeTextBox);
            this.groupBox1.Controls.Add(this._databaseNameTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 129);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CHAMP Server";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._serverIpAddress);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this._agentPasswordTextbox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this._agentcodeTextbox);
            this.groupBox2.Location = new System.Drawing.Point(12, 147);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(323, 101);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DTS Server";
            // 
            // _serverIpAddress
            // 
            this._serverIpAddress.Location = new System.Drawing.Point(120, 15);
            this._serverIpAddress.Name = "_serverIpAddress";
            this._serverIpAddress.Size = new System.Drawing.Size(184, 20);
            this._serverIpAddress.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "DTS Server Address :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Agent Code :";
            // 
            // _agentPasswordTextbox
            // 
            this._agentPasswordTextbox.Location = new System.Drawing.Point(119, 67);
            this._agentPasswordTextbox.Name = "_agentPasswordTextbox";
            this._agentPasswordTextbox.PasswordChar = '*';
            this._agentPasswordTextbox.Size = new System.Drawing.Size(184, 20);
            this._agentPasswordTextbox.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Password :";
            // 
            // _agentcodeTextbox
            // 
            this._agentcodeTextbox.BackColor = System.Drawing.SystemColors.Window;
            this._agentcodeTextbox.Location = new System.Drawing.Point(119, 41);
            this._agentcodeTextbox.Name = "_agentcodeTextbox";
            this._agentcodeTextbox.Size = new System.Drawing.Size(184, 20);
            this._agentcodeTextbox.TabIndex = 19;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 283);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(323, 13);
            this.progressBar1.TabIndex = 22;
            // 
            // _resultLabel
            // 
            this._resultLabel.Location = new System.Drawing.Point(12, 264);
            this._resultLabel.Name = "_resultLabel";
            this._resultLabel.Size = new System.Drawing.Size(323, 13);
            this._resultLabel.TabIndex = 22;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // _serverConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(347, 306);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._connectButton);
            this.Controls.Add(this._closeButton);
            this.Controls.Add(this._saveButton);
            this.Controls.Add(this._resultLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_serverConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect ...";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _saveButton;
        private System.Windows.Forms.TextBox _userPasswordTextBox;
        private System.Windows.Forms.TextBox _userCodeTextBox;
        private System.Windows.Forms.TextBox _databaseNameTextBox;
        private System.Windows.Forms.TextBox _serverNameTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _connectButton;
        private System.Windows.Forms.Button _closeButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox _agentPasswordTextbox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox _agentcodeTextbox;
        private System.Windows.Forms.TextBox _serverIpAddress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label _resultLabel;
        private System.Windows.Forms.Timer timer1;
    }
}