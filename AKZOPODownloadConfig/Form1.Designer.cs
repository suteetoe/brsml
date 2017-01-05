namespace AKZOPODownloadConfig
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._partnTextbox = new System.Windows.Forms.TextBox();
            this._serverIpTextbox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._portTextbox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this._apCodeTextbox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this._testConnectButton = new System.Windows.Forms.Button();
            this._dbNameTextbox = new System.Windows.Forms.TextBox();
            this._userpwTextbox = new System.Windows.Forms.TextBox();
            this._userCodeTextbox = new System.Windows.Forms.TextBox();
            this._hostTextbox = new System.Windows.Forms.TextBox();
            this._closeButton = new System.Windows.Forms.Button();
            this._saveButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "ICI Server Address IP : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Partner ID : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "Database Name : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(109, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "Host : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(80, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "Username : ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(83, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 14);
            this.label6.TabIndex = 5;
            this.label6.Text = "Password : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._partnTextbox);
            this.groupBox1.Controls.Add(this._serverIpTextbox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(589, 87);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ICI Center";
            // 
            // _partnTextbox
            // 
            this._partnTextbox.Location = new System.Drawing.Point(159, 49);
            this._partnTextbox.Name = "_partnTextbox";
            this._partnTextbox.Size = new System.Drawing.Size(250, 22);
            this._partnTextbox.TabIndex = 3;
            // 
            // _serverIpTextbox
            // 
            this._serverIpTextbox.Location = new System.Drawing.Point(159, 21);
            this._serverIpTextbox.Name = "_serverIpTextbox";
            this._serverIpTextbox.Size = new System.Drawing.Size(250, 22);
            this._serverIpTextbox.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._portTextbox);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this._apCodeTextbox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this._testConnectButton);
            this.groupBox2.Controls.Add(this._dbNameTextbox);
            this.groupBox2.Controls.Add(this._userpwTextbox);
            this.groupBox2.Controls.Add(this._userCodeTextbox);
            this.groupBox2.Controls.Add(this._hostTextbox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 105);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(589, 165);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SML Color Store";
            // 
            // _portTextbox
            // 
            this._portTextbox.Location = new System.Drawing.Point(463, 21);
            this._portTextbox.Name = "_portTextbox";
            this._portTextbox.Size = new System.Drawing.Size(82, 22);
            this._portTextbox.TabIndex = 7;
            this._portTextbox.Text = "5432";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(415, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 14);
            this.label8.TabIndex = 13;
            this.label8.Text = "Port : ";
            // 
            // _apCodeTextbox
            // 
            this._apCodeTextbox.Location = new System.Drawing.Point(159, 133);
            this._apCodeTextbox.Name = "_apCodeTextbox";
            this._apCodeTextbox.Size = new System.Drawing.Size(250, 22);
            this._apCodeTextbox.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(59, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 14);
            this.label7.TabIndex = 11;
            this.label7.Text = "Supplier Code : ";
            // 
            // _testConnectButton
            // 
            this._testConnectButton.Image = global::AKZOPODownloadConfig.Properties.Resources.flash;
            this._testConnectButton.Location = new System.Drawing.Point(431, 104);
            this._testConnectButton.Name = "_testConnectButton";
            this._testConnectButton.Size = new System.Drawing.Size(114, 23);
            this._testConnectButton.TabIndex = 11;
            this._testConnectButton.Text = "Test Connect";
            this._testConnectButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._testConnectButton.UseVisualStyleBackColor = true;
            this._testConnectButton.Click += new System.EventHandler(this._testConnectButton_Click);
            // 
            // _dbNameTextbox
            // 
            this._dbNameTextbox.Location = new System.Drawing.Point(159, 105);
            this._dbNameTextbox.Name = "_dbNameTextbox";
            this._dbNameTextbox.Size = new System.Drawing.Size(250, 22);
            this._dbNameTextbox.TabIndex = 10;
            // 
            // _userpwTextbox
            // 
            this._userpwTextbox.Location = new System.Drawing.Point(159, 77);
            this._userpwTextbox.Name = "_userpwTextbox";
            this._userpwTextbox.PasswordChar = '*';
            this._userpwTextbox.Size = new System.Drawing.Size(250, 22);
            this._userpwTextbox.TabIndex = 9;
            // 
            // _userCodeTextbox
            // 
            this._userCodeTextbox.Location = new System.Drawing.Point(159, 49);
            this._userCodeTextbox.Name = "_userCodeTextbox";
            this._userCodeTextbox.Size = new System.Drawing.Size(250, 22);
            this._userCodeTextbox.TabIndex = 8;
            // 
            // _hostTextbox
            // 
            this._hostTextbox.Location = new System.Drawing.Point(159, 21);
            this._hostTextbox.Name = "_hostTextbox";
            this._hostTextbox.Size = new System.Drawing.Size(250, 22);
            this._hostTextbox.TabIndex = 6;
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::AKZOPODownloadConfig.Properties.Resources.delete2;
            this._closeButton.Location = new System.Drawing.Point(445, 276);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(75, 23);
            this._closeButton.TabIndex = 14;
            this._closeButton.Text = "ปิดจอ";
            this._closeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._closeButton.UseVisualStyleBackColor = true;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::AKZOPODownloadConfig.Properties.Resources.disk_blue;
            this._saveButton.Location = new System.Drawing.Point(526, 276);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(75, 23);
            this._saveButton.TabIndex = 13;
            this._saveButton.Text = "บันทึก";
            this._saveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._saveButton.UseVisualStyleBackColor = true;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // button1
            // 
            this.button1.Image = global::AKZOPODownloadConfig.Properties.Resources.delete2;
            this.button1.Location = new System.Drawing.Point(364, 276);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "TEST";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 313);
            this.Controls.Add(this.button1);
            this.Controls.Add(this._closeButton);
            this.Controls.Add(this._saveButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "Form1";
            this.Text = "AKZO PO Download Config";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox _partnTextbox;
        private System.Windows.Forms.TextBox _serverIpTextbox;
        private System.Windows.Forms.TextBox _dbNameTextbox;
        private System.Windows.Forms.TextBox _userpwTextbox;
        private System.Windows.Forms.TextBox _userCodeTextbox;
        private System.Windows.Forms.TextBox _hostTextbox;
        private System.Windows.Forms.Button _saveButton;
        private System.Windows.Forms.Button _closeButton;
        private System.Windows.Forms.Button _testConnectButton;
        private System.Windows.Forms.TextBox _apCodeTextbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox _portTextbox;
        private System.Windows.Forms.Button button1;
    }
}

