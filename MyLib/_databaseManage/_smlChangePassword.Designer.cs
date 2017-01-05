namespace MyLib._databaseManage
{
	partial class _smlChangePassword
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._textBoxOldPassword = new System.Windows.Forms.TextBox();
            this._textBoxNewPassword1 = new System.Windows.Forms.TextBox();
            this._textBoxNewPassword2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._buttonOK = new MyLib._myButton();
            this._myShadowLabel1 = new MyLib._myShadowLabel(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(72, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Old Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(65, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "New Password:";
            // 
            // _textBoxOldPassword
            // 
            this._textBoxOldPassword.Location = new System.Drawing.Point(162, 53);
            this._textBoxOldPassword.Name = "_textBoxOldPassword";
            this._textBoxOldPassword.PasswordChar = '*';
            this._textBoxOldPassword.Size = new System.Drawing.Size(150, 22);
            this._textBoxOldPassword.TabIndex = 2;
            // 
            // _textBoxNewPassword1
            // 
            this._textBoxNewPassword1.Location = new System.Drawing.Point(162, 75);
            this._textBoxNewPassword1.Name = "_textBoxNewPassword1";
            this._textBoxNewPassword1.PasswordChar = '*';
            this._textBoxNewPassword1.Size = new System.Drawing.Size(150, 22);
            this._textBoxNewPassword1.TabIndex = 3;
            // 
            // _textBoxNewPassword2
            // 
            this._textBoxNewPassword2.Location = new System.Drawing.Point(162, 97);
            this._textBoxNewPassword2.Name = "_textBoxNewPassword2";
            this._textBoxNewPassword2.PasswordChar = '*';
            this._textBoxNewPassword2.Size = new System.Drawing.Size(150, 22);
            this._textBoxNewPassword2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(20, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "Confirm New Password:";
            // 
            // _buttonOK
            // 
            this._buttonOK.AutoSize = true;
            this._buttonOK.BackColor = System.Drawing.Color.Transparent;
            this._buttonOK.ButtonText = "เปลี่ยนรหัส";
            this._buttonOK.Location = new System.Drawing.Point(220, 130);
            this._buttonOK.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._buttonOK.myImage = global::MyLib.Resource16x16.disk_blue;
            this._buttonOK.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonOK.myUseVisualStyleBackColor = false;
            this._buttonOK.Name = "_buttonOK";
            this._buttonOK.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._buttonOK.ResourceName = "change_password";
            this._buttonOK.Size = new System.Drawing.Size(91, 24);
            this._buttonOK.TabIndex = 5;
            this._buttonOK.UseVisualStyleBackColor = false;
            this._buttonOK.Click += new System.EventHandler(this._buttonOK_Click);
            // 
            // _myShadowLabel1
            // 
            this._myShadowLabel1.Angle = 0F;
            this._myShadowLabel1.AutoSize = true;
            this._myShadowLabel1.DrawGradient = false;
            this._myShadowLabel1.EndColor = System.Drawing.Color.LightSkyBlue;
            this._myShadowLabel1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myShadowLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this._myShadowLabel1.Location = new System.Drawing.Point(32, 12);
            this._myShadowLabel1.Name = "_myShadowLabel1";
            this._myShadowLabel1.ShadowColor = System.Drawing.Color.Black;
            this._myShadowLabel1.Size = new System.Drawing.Size(271, 25);
            this._myShadowLabel1.StartColor = System.Drawing.Color.White;
            this._myShadowLabel1.TabIndex = 7;
            this._myShadowLabel1.Text = "Change Admin Password";
            this._myShadowLabel1.XOffset = 1F;
            this._myShadowLabel1.YOffset = 1F;
            // 
            // _smlChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(336, 167);
            this.Controls.Add(this._myShadowLabel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._buttonOK);
            this.Controls.Add(this._textBoxNewPassword2);
            this.Controls.Add(this._textBoxNewPassword1);
            this.Controls.Add(this._textBoxOldPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(352, 358);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(352, 180);
            this.Name = "_smlChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_changePassword";
            this.Load += new System.EventHandler(this._changePassword_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox _textBoxOldPassword;
		private System.Windows.Forms.TextBox _textBoxNewPassword1;
		private System.Windows.Forms.TextBox _textBoxNewPassword2;
		private _myButton _buttonOK;
		private System.Windows.Forms.Label label3;
        private _myShadowLabel _myShadowLabel1;

	}
}