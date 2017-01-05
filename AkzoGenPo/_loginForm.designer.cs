namespace AkzoGenPo
{
    partial class _loginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_loginForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._serverNameTextBox = new System.Windows.Forms.TextBox();
            this._databaseNameTextBox = new System.Windows.Forms.TextBox();
            this._userCodeTextBox = new System.Windows.Forms.TextBox();
            this._userPasswordTextBox = new System.Windows.Forms.TextBox();
            this._loginButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Name :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Database Name :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "User Code :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Password :";
            // 
            // _serverNameTextBox
            // 
            this._serverNameTextBox.Location = new System.Drawing.Point(96, 5);
            this._serverNameTextBox.Name = "_serverNameTextBox";
            this._serverNameTextBox.Size = new System.Drawing.Size(184, 21);
            this._serverNameTextBox.TabIndex = 4;
            // 
            // _databaseNameTextBox
            // 
            this._databaseNameTextBox.Location = new System.Drawing.Point(96, 31);
            this._databaseNameTextBox.Name = "_databaseNameTextBox";
            this._databaseNameTextBox.Size = new System.Drawing.Size(184, 21);
            this._databaseNameTextBox.TabIndex = 5;
            // 
            // _userCodeTextBox
            // 
            this._userCodeTextBox.Location = new System.Drawing.Point(96, 58);
            this._userCodeTextBox.Name = "_userCodeTextBox";
            this._userCodeTextBox.Size = new System.Drawing.Size(184, 21);
            this._userCodeTextBox.TabIndex = 6;
            // 
            // _userPasswordTextBox
            // 
            this._userPasswordTextBox.Location = new System.Drawing.Point(96, 84);
            this._userPasswordTextBox.Name = "_userPasswordTextBox";
            this._userPasswordTextBox.PasswordChar = '*';
            this._userPasswordTextBox.Size = new System.Drawing.Size(184, 21);
            this._userPasswordTextBox.TabIndex = 7;
            // 
            // _loginButton
            // 
            this._loginButton.Location = new System.Drawing.Point(204, 112);
            this._loginButton.Name = "_loginButton";
            this._loginButton.Size = new System.Drawing.Size(75, 23);
            this._loginButton.TabIndex = 8;
            this._loginButton.Text = "Login";
            this._loginButton.UseVisualStyleBackColor = true;
            this._loginButton.Click += new System.EventHandler(this._loginButton_Click);
            // 
            // _loginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(285, 139);
            this.Controls.Add(this._loginButton);
            this.Controls.Add(this._userPasswordTextBox);
            this.Controls.Add(this._userCodeTextBox);
            this.Controls.Add(this._databaseNameTextBox);
            this.Controls.Add(this._serverNameTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "_loginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AKZO GEN PO";
            this.Load += new System.EventHandler(this._loginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _serverNameTextBox;
        private System.Windows.Forms.TextBox _databaseNameTextBox;
        private System.Windows.Forms.TextBox _userCodeTextBox;
        private System.Windows.Forms.TextBox _userPasswordTextBox;
        private System.Windows.Forms.Button _loginButton;
    }
}