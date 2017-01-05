namespace MyLib._databaseManage
{
    partial class _passwordForm
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
            this._passwordTextBox = new System.Windows.Forms.TextBox();
            this._okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _passwordTextBox
            // 
            this._passwordTextBox.Location = new System.Drawing.Point(14, 13);
            this._passwordTextBox.Name = "_passwordTextBox";
            this._passwordTextBox.PasswordChar = '*';
            this._passwordTextBox.Size = new System.Drawing.Size(297, 22);
            this._passwordTextBox.TabIndex = 0;
            // 
            // _okButton
            // 
            this._okButton.Location = new System.Drawing.Point(222, 41);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(87, 25);
            this._okButton.TabIndex = 1;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;
            // 
            // _passwordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 79);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._passwordTextBox);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_passwordForm";
            this.Text = "Enter Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox _passwordTextBox;
        public System.Windows.Forms.Button _okButton;

    }
}