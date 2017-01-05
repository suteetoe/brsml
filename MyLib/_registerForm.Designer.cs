namespace MyLib
{
    partial class _register
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
            this._productCodeTextBox = new System.Windows.Forms.TextBox();
            this._registerButton = new System.Windows.Forms.Button();
            this._groupBox = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this._myLabel1 = new MyLib._myLabel();
            this._passwordTextBox = new MyLib._myTextBox();
            this._buttonLoginAdmin = new MyLib._myButton();
            this._webserviceTextBox = new MyLib._myTextBox();
            this._webserviceUrl = new MyLib._myLabel();
            this._groupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product Code :";
            // 
            // _productCodeTextBox
            // 
            this._productCodeTextBox.Location = new System.Drawing.Point(107, 21);
            this._productCodeTextBox.Name = "_productCodeTextBox";
            this._productCodeTextBox.Size = new System.Drawing.Size(333, 22);
            this._productCodeTextBox.TabIndex = 1;
            // 
            // _registerButton
            // 
            this._registerButton.Location = new System.Drawing.Point(446, 20);
            this._registerButton.Name = "_registerButton";
            this._registerButton.Size = new System.Drawing.Size(75, 23);
            this._registerButton.TabIndex = 2;
            this._registerButton.Text = "Register";
            this._registerButton.UseVisualStyleBackColor = true;
            this._registerButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // _groupBox
            // 
            this._groupBox.Controls.Add(this._registerButton);
            this._groupBox.Controls.Add(this.label1);
            this._groupBox.Controls.Add(this._productCodeTextBox);
            this._groupBox.Location = new System.Drawing.Point(9, 76);
            this._groupBox.Name = "_groupBox";
            this._groupBox.Size = new System.Drawing.Size(540, 63);
            this._groupBox.TabIndex = 3;
            this._groupBox.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._myLabel1);
            this.panel1.Controls.Add(this._passwordTextBox);
            this.panel1.Controls.Add(this._buttonLoginAdmin);
            this.panel1.Controls.Add(this._webserviceTextBox);
            this.panel1.Controls.Add(this._webserviceUrl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(561, 70);
            this.panel1.TabIndex = 12;
            // 
            // _myLabel1
            // 
            this._myLabel1.AutoSize = true;
            this._myLabel1.Location = new System.Drawing.Point(6, 42);
            this._myLabel1.Name = "_myLabel1";
            this._myLabel1.ResourceName = "server_password";
            this._myLabel1.Size = new System.Drawing.Size(86, 14);
            this._myLabel1.TabIndex = 0;
            this._myLabel1.Text = "รหัสผ่าน Server";
            // 
            // _passwordTextBox
            // 
            this._passwordTextBox._column = 0;
            this._passwordTextBox._defaultBackGround = System.Drawing.Color.White;
            this._passwordTextBox._emtry = true;
            this._passwordTextBox._enterToTab = false;
            this._passwordTextBox._icon = false;
            this._passwordTextBox._iconNumber = 1;
            this._passwordTextBox._isChange = false;
            this._passwordTextBox._isQuery = true;
            this._passwordTextBox._isSearch = false;
            this._passwordTextBox._isTime = false;
            this._passwordTextBox._labelName = "";
            this._passwordTextBox._maxColumn = 0;
            this._passwordTextBox._name = null;
            this._passwordTextBox._row = 0;
            this._passwordTextBox._rowCount = 0;
            this._passwordTextBox._textFirst = "";
            this._passwordTextBox._textLast = "";
            this._passwordTextBox._textSecond = "";
            this._passwordTextBox._upperCase = false;
            this._passwordTextBox.BackColor = System.Drawing.SystemColors.Window;
            this._passwordTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._passwordTextBox.ForeColor = System.Drawing.Color.Black;
            this._passwordTextBox.IsUpperCase = false;
            this._passwordTextBox.Location = new System.Drawing.Point(113, 36);
            this._passwordTextBox.Margin = new System.Windows.Forms.Padding(0);
            this._passwordTextBox.MaxLength = 0;
            this._passwordTextBox.Name = "_passwordTextBox";
            this._passwordTextBox.ShowIcon = false;
            this._passwordTextBox.Size = new System.Drawing.Size(349, 22);
            this._passwordTextBox.TabIndex = 2;
            // 
            // _buttonLoginAdmin
            // 
            this._buttonLoginAdmin.AutoSize = true;
            this._buttonLoginAdmin.BackColor = System.Drawing.Color.Transparent;
            this._buttonLoginAdmin.ButtonText = "เข้าสู่ระบบ";
            this._buttonLoginAdmin.Location = new System.Drawing.Point(468, 35);
            this._buttonLoginAdmin.Margin = new System.Windows.Forms.Padding(2);
            this._buttonLoginAdmin.myImage = global::MyLib.Resource16x16.key1;
            this._buttonLoginAdmin.myTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._buttonLoginAdmin.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonLoginAdmin.myUseVisualStyleBackColor = false;
            this._buttonLoginAdmin.Name = "_buttonLoginAdmin";
            this._buttonLoginAdmin.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this._buttonLoginAdmin.ResourceName = "login";
            this._buttonLoginAdmin.Size = new System.Drawing.Size(87, 24);
            this._buttonLoginAdmin.TabIndex = 3;
            this._buttonLoginAdmin.Text = "Login";
            this._buttonLoginAdmin.UseVisualStyleBackColor = false;
            this._buttonLoginAdmin.Click += new System.EventHandler(this._buttonLoginAdmin_Click);
            // 
            // _webserviceTextBox
            // 
            this._webserviceTextBox._column = 0;
            this._webserviceTextBox._defaultBackGround = System.Drawing.Color.White;
            this._webserviceTextBox._emtry = true;
            this._webserviceTextBox._enterToTab = false;
            this._webserviceTextBox._icon = false;
            this._webserviceTextBox._iconNumber = 1;
            this._webserviceTextBox._isChange = false;
            this._webserviceTextBox._isQuery = true;
            this._webserviceTextBox._isSearch = false;
            this._webserviceTextBox._isTime = false;
            this._webserviceTextBox._labelName = "";
            this._webserviceTextBox._maxColumn = 0;
            this._webserviceTextBox._name = null;
            this._webserviceTextBox._row = 0;
            this._webserviceTextBox._rowCount = 0;
            this._webserviceTextBox._textFirst = "";
            this._webserviceTextBox._textLast = "";
            this._webserviceTextBox._textSecond = "";
            this._webserviceTextBox._upperCase = false;
            this._webserviceTextBox.BackColor = System.Drawing.SystemColors.Window;
            this._webserviceTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._webserviceTextBox.ForeColor = System.Drawing.Color.Black;
            this._webserviceTextBox.IsUpperCase = false;
            this._webserviceTextBox.Location = new System.Drawing.Point(113, 9);
            this._webserviceTextBox.Margin = new System.Windows.Forms.Padding(0);
            this._webserviceTextBox.MaxLength = 0;
            this._webserviceTextBox.Name = "_webserviceTextBox";
            this._webserviceTextBox.ShowIcon = false;
            this._webserviceTextBox.Size = new System.Drawing.Size(349, 22);
            this._webserviceTextBox.TabIndex = 1;
            // 
            // _webserviceUrl
            // 
            this._webserviceUrl.AutoSize = true;
            this._webserviceUrl.Location = new System.Drawing.Point(9, 14);
            this._webserviceUrl.Name = "_webserviceUrl";
            this._webserviceUrl.ResourceName = "webservice_url";
            this._webserviceUrl.Size = new System.Drawing.Size(101, 14);
            this._webserviceUrl.TabIndex = 10;
            this._webserviceUrl.Text = "Web Service URL";
            // 
            // _register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 154);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._groupBox);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "_register";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register";
            this._groupBox.ResumeLayout(false);
            this._groupBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _productCodeTextBox;
        private System.Windows.Forms.Button _registerButton;
        private System.Windows.Forms.GroupBox _groupBox;
        private System.Windows.Forms.Panel panel1;
        private _myLabel _myLabel1;
        private _myTextBox _passwordTextBox;
        private _myButton _buttonLoginAdmin;
        private _myTextBox _webserviceTextBox;
        private _myLabel _webserviceUrl;
    }
}