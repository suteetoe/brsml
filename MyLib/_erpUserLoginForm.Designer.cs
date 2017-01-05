namespace MyLib
{
    partial class _erpUserLoginForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._loginButton = new System.Windows.Forms.Button();
            this._passwordTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._userCodeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MyLib.Properties.Resources.keys_48;
            this.pictureBox1.Location = new System.Drawing.Point(8, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // _loginButton
            // 
            this._loginButton.Location = new System.Drawing.Point(256, 68);
            this._loginButton.Name = "_loginButton";
            this._loginButton.Size = new System.Drawing.Size(87, 25);
            this._loginButton.TabIndex = 10;
            this._loginButton.Text = "Login";
            this._loginButton.UseVisualStyleBackColor = true;
            this._loginButton.Click += new System.EventHandler(this._loginButton_Click);
            // 
            // _passwordTextBox
            // 
            this._passwordTextBox.Location = new System.Drawing.Point(131, 40);
            this._passwordTextBox.Name = "_passwordTextBox";
            this._passwordTextBox.PasswordChar = '*';
            this._passwordTextBox.Size = new System.Drawing.Size(212, 22);
            this._passwordTextBox.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 14);
            this.label2.TabIndex = 8;
            this.label2.Text = "Password :";
            // 
            // _userCodeTextBox
            // 
            this._userCodeTextBox.Location = new System.Drawing.Point(131, 10);
            this._userCodeTextBox.Name = "_userCodeTextBox";
            this._userCodeTextBox.Size = new System.Drawing.Size(212, 22);
            this._userCodeTextBox.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "User Code :";
            // 
            // _erpUserLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(350, 106);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this._loginButton);
            this.Controls.Add(this._passwordTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._userCodeTextBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_erpUserLoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.TextBox _passwordTextBox;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox _userCodeTextBox;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button _loginButton;
    }
}