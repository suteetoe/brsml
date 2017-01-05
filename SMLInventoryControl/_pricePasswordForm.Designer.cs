namespace SMLInventoryControl
{
    partial class _pricePasswordForm
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
            this._userTextBox = new System.Windows.Forms.TextBox();
            this._passwordTextBox = new System.Windows.Forms.TextBox();
            this._processButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "User :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password :";
            // 
            // _userTextBox
            // 
            this._userTextBox.Location = new System.Drawing.Point(85, 13);
            this._userTextBox.Name = "_userTextBox";
            this._userTextBox.Size = new System.Drawing.Size(172, 22);
            this._userTextBox.TabIndex = 4;
            // 
            // _passwordTextBox
            // 
            this._passwordTextBox.Location = new System.Drawing.Point(85, 41);
            this._passwordTextBox.Name = "_passwordTextBox";
            this._passwordTextBox.PasswordChar = '*';
            this._passwordTextBox.Size = new System.Drawing.Size(172, 22);
            this._passwordTextBox.TabIndex = 5;
            // 
            // _processButton
            // 
            this._processButton.Image = global::SMLInventoryControl.Properties.Resources.flash;
            this._processButton.Location = new System.Drawing.Point(149, 69);
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(108, 23);
            this._processButton.TabIndex = 3;
            this._processButton.Text = "Process (F12)";
            this._processButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._processButton.UseVisualStyleBackColor = true;
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.Image = global::SMLInventoryControl.Properties.Resources.error1;
            this._cancelButton.Location = new System.Drawing.Point(43, 69);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(100, 23);
            this._cancelButton.TabIndex = 2;
            this._cancelButton.Text = "Cancel (Esc)";
            this._cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _pricePasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 98);
            this.Controls.Add(this._passwordTextBox);
            this.Controls.Add(this._userTextBox);
            this.Controls.Add(this._processButton);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_pricePasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _cancelButton;
        public System.Windows.Forms.Button _processButton;
        public System.Windows.Forms.TextBox _userTextBox;
        public System.Windows.Forms.TextBox _passwordTextBox;
    }
}