namespace MyLib._databaseManage
{
    partial class _emergencyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_emergencyForm));
            this._myPanel1 = new MyLib._myPanel();
            this._portTextbox = new System.Windows.Forms.TextBox();
            this._myLabel3 = new MyLib._myLabel();
            this._passTextbox = new System.Windows.Forms.TextBox();
            this._userTextbox = new System.Windows.Forms.TextBox();
            this._myLabel2 = new MyLib._myLabel();
            this._myLabel1 = new MyLib._myLabel();
            this._loginButton = new MyLib.VistaButton();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._portTextbox);
            this._myPanel1.Controls.Add(this._myLabel3);
            this._myPanel1.Controls.Add(this._passTextbox);
            this._myPanel1.Controls.Add(this._userTextbox);
            this._myPanel1.Controls.Add(this._myLabel2);
            this._myPanel1.Controls.Add(this._myLabel1);
            this._myPanel1.Controls.Add(this._loginButton);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(364, 134);
            this._myPanel1.TabIndex = 1;
            // 
            // _portTextbox
            // 
            this._portTextbox.Location = new System.Drawing.Point(91, 68);
            this._portTextbox.Name = "_portTextbox";
            this._portTextbox.Size = new System.Drawing.Size(74, 22);
            this._portTextbox.TabIndex = 6;
            this._portTextbox.Text = "8080";
            // 
            // _myLabel3
            // 
            this._myLabel3.AutoSize = true;
            this._myLabel3.BackColor = System.Drawing.Color.Transparent;
            this._myLabel3.Location = new System.Drawing.Point(47, 71);
            this._myLabel3.Name = "_myLabel3";
            this._myLabel3.ResourceName = "";
            this._myLabel3.Size = new System.Drawing.Size(38, 14);
            this._myLabel3.TabIndex = 5;
            this._myLabel3.Text = "Port :";
            // 
            // _passTextbox
            // 
            this._passTextbox.Location = new System.Drawing.Point(91, 40);
            this._passTextbox.Name = "_passTextbox";
            this._passTextbox.PasswordChar = '*';
            this._passTextbox.Size = new System.Drawing.Size(262, 22);
            this._passTextbox.TabIndex = 4;
            // 
            // _userTextbox
            // 
            this._userTextbox.Location = new System.Drawing.Point(91, 12);
            this._userTextbox.Name = "_userTextbox";
            this._userTextbox.Size = new System.Drawing.Size(262, 22);
            this._userTextbox.TabIndex = 3;
            // 
            // _myLabel2
            // 
            this._myLabel2.AutoSize = true;
            this._myLabel2.BackColor = System.Drawing.Color.Transparent;
            this._myLabel2.Location = new System.Drawing.Point(19, 43);
            this._myLabel2.Name = "_myLabel2";
            this._myLabel2.ResourceName = "";
            this._myLabel2.Size = new System.Drawing.Size(66, 14);
            this._myLabel2.TabIndex = 2;
            this._myLabel2.Text = "Password :";
            // 
            // _myLabel1
            // 
            this._myLabel1.AutoSize = true;
            this._myLabel1.BackColor = System.Drawing.Color.Transparent;
            this._myLabel1.Location = new System.Drawing.Point(16, 15);
            this._myLabel1.Name = "_myLabel1";
            this._myLabel1.ResourceName = "";
            this._myLabel1.Size = new System.Drawing.Size(69, 14);
            this._myLabel1.TabIndex = 1;
            this._myLabel1.Text = "Usrename :";
            // 
            // _loginButton
            // 
            this._loginButton._drawNewMethod = false;
            this._loginButton.BackColor = System.Drawing.Color.Transparent;
            this._loginButton.ButtonText = "Login";
            this._loginButton.Location = new System.Drawing.Point(274, 95);
            this._loginButton.myImage = global::MyLib.Properties.Resources._lock;
            this._loginButton.Name = "_loginButton";
            this._loginButton.Size = new System.Drawing.Size(78, 27);
            this._loginButton.TabIndex = 0;
            this._loginButton.Text = "vistaButton1";
            this._loginButton.UseVisualStyleBackColor = false;
            // 
            // _emergencyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 134);
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_emergencyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " เข้าสู่โหลดฉุกเฉิน";
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private _myPanel _myPanel1;
        private _myLabel _myLabel2;
        private _myLabel _myLabel1;
        public System.Windows.Forms.TextBox _passTextbox;
        public System.Windows.Forms.TextBox _userTextbox;
        public VistaButton _loginButton;
        public System.Windows.Forms.TextBox _portTextbox;
        private _myLabel _myLabel3;
    }
}