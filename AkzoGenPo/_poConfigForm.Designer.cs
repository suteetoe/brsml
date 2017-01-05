namespace AkzoGenPo
{
    partial class _poConfigForm
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
            this._myPanel1 = new MyLib._myPanel();
            this._saveButton = new System.Windows.Forms.Button();
            this._emailTextbox = new System.Windows.Forms.TextBox();
            this._passwordTextbox = new System.Windows.Forms.TextBox();
            this._usercodeTextbox = new System.Windows.Forms.TextBox();
            this._databaseTextbox = new System.Windows.Forms.TextBox();
            this._serverTextbox = new System.Windows.Forms.TextBox();
            this._sendmailCheckbox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._passwordSenderTextbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this.label5);
            this._myPanel1.Controls.Add(this._passwordSenderTextbox);
            this._myPanel1.Controls.Add(this._saveButton);
            this._myPanel1.Controls.Add(this._emailTextbox);
            this._myPanel1.Controls.Add(this._passwordTextbox);
            this._myPanel1.Controls.Add(this._usercodeTextbox);
            this._myPanel1.Controls.Add(this._databaseTextbox);
            this._myPanel1.Controls.Add(this._serverTextbox);
            this._myPanel1.Controls.Add(this._sendmailCheckbox);
            this._myPanel1.Controls.Add(this.label6);
            this._myPanel1.Controls.Add(this.label4);
            this._myPanel1.Controls.Add(this.label3);
            this._myPanel1.Controls.Add(this.label2);
            this._myPanel1.Controls.Add(this.label1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(459, 226);
            this._myPanel1.TabIndex = 0;
            // 
            // _saveButton
            // 
            this._saveButton.Location = new System.Drawing.Point(327, 191);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(120, 23);
            this._saveButton.TabIndex = 13;
            this._saveButton.Text = "Save Config";
            this._saveButton.UseVisualStyleBackColor = true;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _emailTextbox
            // 
            this._emailTextbox.Location = new System.Drawing.Point(122, 116);
            this._emailTextbox.Name = "_emailTextbox";
            this._emailTextbox.Size = new System.Drawing.Size(325, 22);
            this._emailTextbox.TabIndex = 12;
            // 
            // _passwordTextbox
            // 
            this._passwordTextbox.Location = new System.Drawing.Point(122, 90);
            this._passwordTextbox.Name = "_passwordTextbox";
            this._passwordTextbox.PasswordChar = '*';
            this._passwordTextbox.Size = new System.Drawing.Size(325, 22);
            this._passwordTextbox.TabIndex = 10;
            // 
            // _usercodeTextbox
            // 
            this._usercodeTextbox.Location = new System.Drawing.Point(122, 64);
            this._usercodeTextbox.Name = "_usercodeTextbox";
            this._usercodeTextbox.Size = new System.Drawing.Size(325, 22);
            this._usercodeTextbox.TabIndex = 9;
            // 
            // _databaseTextbox
            // 
            this._databaseTextbox.Location = new System.Drawing.Point(122, 38);
            this._databaseTextbox.Name = "_databaseTextbox";
            this._databaseTextbox.Size = new System.Drawing.Size(325, 22);
            this._databaseTextbox.TabIndex = 8;
            // 
            // _serverTextbox
            // 
            this._serverTextbox.Location = new System.Drawing.Point(122, 12);
            this._serverTextbox.Name = "_serverTextbox";
            this._serverTextbox.Size = new System.Drawing.Size(325, 22);
            this._serverTextbox.TabIndex = 7;
            // 
            // _sendmailCheckbox
            // 
            this._sendmailCheckbox.AutoSize = true;
            this._sendmailCheckbox.BackColor = System.Drawing.Color.Transparent;
            this._sendmailCheckbox.Location = new System.Drawing.Point(122, 168);
            this._sendmailCheckbox.Name = "_sendmailCheckbox";
            this._sendmailCheckbox.Size = new System.Drawing.Size(194, 18);
            this._sendmailCheckbox.TabIndex = 6;
            this._sendmailCheckbox.Text = "Send Email After Gen PO Form";
            this._sendmailCheckbox.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(31, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 14);
            this.label6.TabIndex = 5;
            this.label6.Text = "Email Sender :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(50, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "Password :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(45, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "User Code :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(16, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Database Name :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(66, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server :";
            // 
            // _passwordSenderTextbox
            // 
            this._passwordSenderTextbox.Location = new System.Drawing.Point(122, 142);
            this._passwordSenderTextbox.Name = "_passwordSenderTextbox";
            this._passwordSenderTextbox.PasswordChar = '*';
            this._passwordSenderTextbox.Size = new System.Drawing.Size(325, 22);
            this._passwordSenderTextbox.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(7, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 14);
            this.label5.TabIndex = 15;
            this.label5.Text = "Password Sender :";
            // 
            // _poConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 226);
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_poConfigForm";
            this.Text = "Gen PO Config";
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _saveButton;
        private System.Windows.Forms.TextBox _emailTextbox;
        private System.Windows.Forms.TextBox _passwordTextbox;
        private System.Windows.Forms.TextBox _usercodeTextbox;
        private System.Windows.Forms.TextBox _databaseTextbox;
        private System.Windows.Forms.TextBox _serverTextbox;
        private System.Windows.Forms.CheckBox _sendmailCheckbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox _passwordSenderTextbox;
    }
}