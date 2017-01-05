namespace SMLPosClient
{
    partial class _posConfimUserForm
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
            this._cancelButton = new System.Windows.Forms.Button();
            this._passwordTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._userCodeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._okButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // _cancelButton
            // 
            this._cancelButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._cancelButton.Location = new System.Drawing.Point(242, 65);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 10;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _passwordTextBox
            // 
            this._passwordTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._passwordTextBox.Location = new System.Drawing.Point(135, 35);
            this._passwordTextBox.Name = "_passwordTextBox";
            this._passwordTextBox.PasswordChar = '*';
            this._passwordTextBox.Size = new System.Drawing.Size(182, 22);
            this._passwordTextBox.TabIndex = 9;
            this._passwordTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this._passwordTextBox_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(63, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 14);
            this.label2.TabIndex = 8;
            this.label2.Text = "Password :";
            // 
            // _userCodeTextBox
            // 
            this._userCodeTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._userCodeTextBox.Location = new System.Drawing.Point(135, 7);
            this._userCodeTextBox.Name = "_userCodeTextBox";
            this._userCodeTextBox.Size = new System.Drawing.Size(182, 22);
            this._userCodeTextBox.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(58, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "User Code :";
            // 
            // _okButton
            // 
            this._okButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._okButton.Location = new System.Drawing.Point(161, 66);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 12;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SMLPosClient.Properties.Resources.locked;
            this.pictureBox1.Location = new System.Drawing.Point(7, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // _posConfimUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(323, 92);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._passwordTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._userCodeTextBox);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_posConfimUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter User Password";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button _cancelButton;
        public System.Windows.Forms.TextBox _passwordTextBox;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox _userCodeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _okButton;
    }
}