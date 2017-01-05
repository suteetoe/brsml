namespace SMLERPConfig
{
    partial class _dashboardConfigScreen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_dashboardConfigScreen));
            this.panel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._registerButton = new MyLib._myButton();
            this._saveEditPassword = new MyLib._myButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this._checkShopIDButton = new MyLib._myButton();
            this._loginShopButton = new MyLib._myButton();
            this._screen = new MyLib._myScreen();
            this.panel3 = new System.Windows.Forms.FlowLayoutPanel();
            this._registerNewShopButton = new MyLib._myButton();
            this._registerOldShowButton = new MyLib._myButton();
            this._editPasswordButton = new MyLib._myButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._registerButton);
            this.panel1.Controls.Add(this._saveEditPassword);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(507, 44);
            this.panel1.TabIndex = 1;
            // 
            // _registerButton
            // 
            this._registerButton._drawNewMethod = false;
            this._registerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._registerButton.AutoSize = true;
            this._registerButton.BackColor = System.Drawing.Color.Transparent;
            this._registerButton.ButtonText = "ลงทะเบียน";
            this._registerButton.Location = new System.Drawing.Point(416, 0);
            this._registerButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._registerButton.myImage = global::SMLERPConfig.Properties.Resources.flash2;
            this._registerButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._registerButton.myUseVisualStyleBackColor = false;
            this._registerButton.Name = "_registerButton";
            this._registerButton.ResourceName = "ลงทะเบียน";
            this._registerButton.Size = new System.Drawing.Size(90, 24);
            this._registerButton.TabIndex = 0;
            this._registerButton.Text = "ลงทะเบียน";
            this._registerButton.UseVisualStyleBackColor = false;
            this._registerButton.Visible = false;
            this._registerButton.Click += new System.EventHandler(this._registerButton_Click);
            // 
            // _saveEditPassword
            // 
            this._saveEditPassword._drawNewMethod = false;
            this._saveEditPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._saveEditPassword.AutoSize = true;
            this._saveEditPassword.BackColor = System.Drawing.Color.Transparent;
            this._saveEditPassword.ButtonText = "บันทึก";
            this._saveEditPassword.Location = new System.Drawing.Point(347, 0);
            this._saveEditPassword.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._saveEditPassword.myImage = global::SMLERPConfig.Properties.Resources.flash2;
            this._saveEditPassword.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._saveEditPassword.myUseVisualStyleBackColor = false;
            this._saveEditPassword.Name = "_saveEditPassword";
            this._saveEditPassword.ResourceName = "บันทึก";
            this._saveEditPassword.Size = new System.Drawing.Size(67, 24);
            this._saveEditPassword.TabIndex = 1;
            this._saveEditPassword.Text = "บันทึก";
            this._saveEditPassword.UseVisualStyleBackColor = false;
            this._saveEditPassword.Visible = false;
            this._saveEditPassword.Click += new System.EventHandler(this._saveEditPassword_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._checkShopIDButton);
            this.panel2.Controls.Add(this._loginShopButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(507, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(136, 427);
            this.panel2.TabIndex = 2;
            // 
            // _checkShopIDButton
            // 
            this._checkShopIDButton._drawNewMethod = false;
            this._checkShopIDButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._checkShopIDButton.AutoSize = true;
            this._checkShopIDButton.BackColor = System.Drawing.Color.Transparent;
            this._checkShopIDButton.ButtonText = "ตรวจสอบรหัส";
            this._checkShopIDButton.Location = new System.Drawing.Point(6, 23);
            this._checkShopIDButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._checkShopIDButton.myImage = global::SMLERPConfig.Properties.Resources.checks;
            this._checkShopIDButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._checkShopIDButton.myUseVisualStyleBackColor = false;
            this._checkShopIDButton.Name = "_checkShopIDButton";
            this._checkShopIDButton.ResourceName = "ตรวจสอบรหัส";
            this._checkShopIDButton.Size = new System.Drawing.Size(105, 24);
            this._checkShopIDButton.TabIndex = 2;
            this._checkShopIDButton.Text = "ตรวจสอบรหัส";
            this._checkShopIDButton.UseVisualStyleBackColor = false;
            this._checkShopIDButton.Visible = false;
            this._checkShopIDButton.Click += new System.EventHandler(this._checkShopIDButton_Click);
            // 
            // _loginShopButton
            // 
            this._loginShopButton._drawNewMethod = false;
            this._loginShopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._loginShopButton.AutoSize = true;
            this._loginShopButton.BackColor = System.Drawing.Color.Transparent;
            this._loginShopButton.ButtonText = "เข้าสู่ระบบ";
            this._loginShopButton.Location = new System.Drawing.Point(6, 46);
            this._loginShopButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._loginShopButton.myImage = ((System.Drawing.Image)(resources.GetObject("_loginShopButton.myImage")));
            this._loginShopButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._loginShopButton.myUseVisualStyleBackColor = false;
            this._loginShopButton.Name = "_loginShopButton";
            this._loginShopButton.ResourceName = "เข้าสู่ระบบ";
            this._loginShopButton.Size = new System.Drawing.Size(87, 24);
            this._loginShopButton.TabIndex = 1;
            this._loginShopButton.Text = "เข้าสู่ระบบ";
            this._loginShopButton.UseVisualStyleBackColor = false;
            this._loginShopButton.Visible = false;
            this._loginShopButton.Click += new System.EventHandler(this._loginShopButton_Click);
            // 
            // _screen
            // 
            this._screen._isChange = false;
            this._screen.BackColor = System.Drawing.Color.Transparent;
            this._screen.Dock = System.Windows.Forms.DockStyle.Top;
            this._screen.Location = new System.Drawing.Point(0, 39);
            this._screen.Name = "_screen";
            this._screen.Size = new System.Drawing.Size(507, 11);
            this._screen.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this._registerNewShopButton);
            this.panel3.Controls.Add(this._registerOldShowButton);
            this.panel3.Controls.Add(this._editPasswordButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(643, 39);
            this.panel3.TabIndex = 3;
            // 
            // _registerNewShopButton
            // 
            this._registerNewShopButton._drawNewMethod = false;
            this._registerNewShopButton.AutoSize = true;
            this._registerNewShopButton.BackColor = System.Drawing.Color.Transparent;
            this._registerNewShopButton.ButtonText = "ลงทะเบียนร้านค้าใหม่";
            this._registerNewShopButton.Location = new System.Drawing.Point(1, 0);
            this._registerNewShopButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._registerNewShopButton.myImage = ((System.Drawing.Image)(resources.GetObject("_registerNewShopButton.myImage")));
            this._registerNewShopButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._registerNewShopButton.myUseVisualStyleBackColor = false;
            this._registerNewShopButton.Name = "_registerNewShopButton";
            this._registerNewShopButton.ResourceName = "ลงทะเบียนร้านค้าใหม่";
            this._registerNewShopButton.Size = new System.Drawing.Size(144, 24);
            this._registerNewShopButton.TabIndex = 2;
            this._registerNewShopButton.Text = "ลงทะเบียนร้านค้าใหม่";
            this._registerNewShopButton.UseVisualStyleBackColor = false;
            this._registerNewShopButton.Click += new System.EventHandler(this._registerNewShopButton_Click);
            // 
            // _registerOldShowButton
            // 
            this._registerOldShowButton._drawNewMethod = false;
            this._registerOldShowButton.AutoSize = true;
            this._registerOldShowButton.BackColor = System.Drawing.Color.Transparent;
            this._registerOldShowButton.ButtonText = "ลงทะเบียนร้านค้าเพิ่ม";
            this._registerOldShowButton.Location = new System.Drawing.Point(147, 0);
            this._registerOldShowButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._registerOldShowButton.myImage = global::SMLERPConfig.Properties.Resources.node_add;
            this._registerOldShowButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._registerOldShowButton.myUseVisualStyleBackColor = false;
            this._registerOldShowButton.Name = "_registerOldShowButton";
            this._registerOldShowButton.ResourceName = "ลงทะเบียนร้านค้าเพิ่ม";
            this._registerOldShowButton.Size = new System.Drawing.Size(143, 24);
            this._registerOldShowButton.TabIndex = 1;
            this._registerOldShowButton.Text = "ลงทะเบียนร้านค้าเพิ่ม";
            this._registerOldShowButton.UseVisualStyleBackColor = false;
            this._registerOldShowButton.Click += new System.EventHandler(this._registerOldShowButton_Click);
            // 
            // _editPasswordButton
            // 
            this._editPasswordButton._drawNewMethod = false;
            this._editPasswordButton.AutoSize = true;
            this._editPasswordButton.BackColor = System.Drawing.Color.Transparent;
            this._editPasswordButton.ButtonText = "แก้ไขรหัสผ่าน";
            this._editPasswordButton.Location = new System.Drawing.Point(292, 0);
            this._editPasswordButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._editPasswordButton.myImage = global::SMLERPConfig.Resource16x16.lightbulb_on;
            this._editPasswordButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._editPasswordButton.myUseVisualStyleBackColor = false;
            this._editPasswordButton.Name = "_editPasswordButton";
            this._editPasswordButton.ResourceName = "แก้ไขรหัสผ่าน";
            this._editPasswordButton.Size = new System.Drawing.Size(107, 24);
            this._editPasswordButton.TabIndex = 3;
            this._editPasswordButton.Text = "แก้ไขรหัสผ่าน";
            this._editPasswordButton.UseVisualStyleBackColor = false;
            this._editPasswordButton.Click += new System.EventHandler(this._editPasswordButton_Click);
            // 
            // _dashboardConfigScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._screen);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_dashboardConfigScreen";
            this.Size = new System.Drawing.Size(643, 466);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panel1;
        private MyLib._myButton _registerButton;
        public MyLib._myScreen _screen;
        private System.Windows.Forms.Panel panel2;
        private MyLib._myButton _checkShopIDButton;
        private MyLib._myButton _loginShopButton;
        private System.Windows.Forms.FlowLayoutPanel panel3;
        private MyLib._myButton _registerNewShopButton;
        private MyLib._myButton _registerOldShowButton;
        private MyLib._myButton _editPasswordButton;
        private MyLib._myButton _saveEditPassword;
    }
}
