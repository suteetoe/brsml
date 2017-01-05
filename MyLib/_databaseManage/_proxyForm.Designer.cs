namespace MyLib._databaseManage
{
    partial class _proxyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_proxyForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._urlTextBox = new System.Windows.Forms.TextBox();
            this._userTextBox = new System.Windows.Forms.TextBox();
            this._passwordTextBox = new System.Windows.Forms.TextBox();
            this._saveButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this._useProxyCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Proxy Server :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "User :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password :";
            // 
            // _urlTextBox
            // 
            this._urlTextBox.Location = new System.Drawing.Point(111, 38);
            this._urlTextBox.Name = "_urlTextBox";
            this._urlTextBox.Size = new System.Drawing.Size(305, 22);
            this._urlTextBox.TabIndex = 2;
            // 
            // _userTextBox
            // 
            this._userTextBox.Location = new System.Drawing.Point(111, 66);
            this._userTextBox.Name = "_userTextBox";
            this._userTextBox.Size = new System.Drawing.Size(194, 22);
            this._userTextBox.TabIndex = 3;
            // 
            // _passwordTextBox
            // 
            this._passwordTextBox.Location = new System.Drawing.Point(111, 94);
            this._passwordTextBox.Name = "_passwordTextBox";
            this._passwordTextBox.PasswordChar = '*';
            this._passwordTextBox.Size = new System.Drawing.Size(194, 22);
            this._passwordTextBox.TabIndex = 4;
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::MyLib.Properties.Resources.flash1;
            this._saveButton.Location = new System.Drawing.Point(341, 124);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(75, 23);
            this._saveButton.TabIndex = 5;
            this._saveButton.Text = "Save";
            this._saveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._saveButton.UseVisualStyleBackColor = true;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.Image = global::MyLib.Properties.Resources.error1;
            this._cancelButton.Location = new System.Drawing.Point(260, 124);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 6;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "Use Proxy :";
            // 
            // _useProxyCheckBox
            // 
            this._useProxyCheckBox.AutoSize = true;
            this._useProxyCheckBox.Location = new System.Drawing.Point(111, 11);
            this._useProxyCheckBox.Name = "_useProxyCheckBox";
            this._useProxyCheckBox.Size = new System.Drawing.Size(15, 14);
            this._useProxyCheckBox.TabIndex = 1;
            this._useProxyCheckBox.UseVisualStyleBackColor = true;
            // 
            // _proxyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 156);
            this.ControlBox = false;
            this.Controls.Add(this._useProxyCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._saveButton);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._passwordTextBox);
            this.Controls.Add(this._userTextBox);
            this.Controls.Add(this._urlTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "_proxyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proxy";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _urlTextBox;
        private System.Windows.Forms.TextBox _userTextBox;
        private System.Windows.Forms.TextBox _passwordTextBox;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.Button _saveButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox _useProxyCheckBox;

    }
}