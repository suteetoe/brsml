namespace MyLib._databaseManage
{
    partial class _providerConfig
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
            this._webserviceUrl = new MyLib._myLabel();
            this._webserviceTextBox = new MyLib._myTextBox();
            this._saveButton = new MyLib._myButton();
            this._closeButton = new MyLib._myButton();
            this._providerGrid = new MyLib._myGrid();
            this._buttonLoginAdmin = new MyLib._myButton();
            this._passwordTextBox = new MyLib._myTextBox();
            this._myLabel1 = new MyLib._myLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _webserviceUrl
            // 
            this._webserviceUrl.AutoSize = true;
            this._webserviceUrl.Location = new System.Drawing.Point(9, 14);
            this._webserviceUrl.Name = "_webserviceUrl";
            this._webserviceUrl.ResourceName = "webservice_url";
            this._webserviceUrl.Size = new System.Drawing.Size(79, 13);
            this._webserviceUrl.TabIndex = 10;
            this._webserviceUrl.Text = "webservice_url";
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
            this._webserviceTextBox.Location = new System.Drawing.Point(104, 9);
            this._webserviceTextBox.Margin = new System.Windows.Forms.Padding(0);
            this._webserviceTextBox.MaxLength = 0;
            this._webserviceTextBox.Name = "_webserviceTextBox";
            this._webserviceTextBox.ShowIcon = false;
            this._webserviceTextBox.Size = new System.Drawing.Size(418, 22);
            this._webserviceTextBox.TabIndex = 1;
            // 
            // _saveButton
            // 
            this._saveButton._drawNewMethod = false;
            this._saveButton.AutoSize = true;
            this._saveButton.BackColor = System.Drawing.Color.Transparent;
            this._saveButton.ButtonText = "save";
            this._saveButton.Location = new System.Drawing.Point(557, 2);
            this._saveButton.Margin = new System.Windows.Forms.Padding(2);
            this._saveButton.myImage = global::MyLib.Resource16x16.check2;
            this._saveButton.myTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._saveButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._saveButton.myUseVisualStyleBackColor = false;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this._saveButton.ResourceName = "save";
            this._saveButton.Size = new System.Drawing.Size(58, 24);
            this._saveButton.TabIndex = 5;
            this._saveButton.Text = "save";
            this._saveButton.UseVisualStyleBackColor = false;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton._drawNewMethod = false;
            this._closeButton.AutoSize = true;
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.ButtonText = "screen_close";
            this._closeButton.Location = new System.Drawing.Point(454, 2);
            this._closeButton.Margin = new System.Windows.Forms.Padding(2);
            this._closeButton.myImage = global::MyLib.Resource16x16.error1;
            this._closeButton.myTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._closeButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._closeButton.myUseVisualStyleBackColor = false;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this._closeButton.ResourceName = "screen_close";
            this._closeButton.Size = new System.Drawing.Size(99, 24);
            this._closeButton.TabIndex = 6;
            this._closeButton.Text = "screen_close";
            this._closeButton.UseVisualStyleBackColor = false;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _providerGrid
            // 
            this._providerGrid._extraWordShow = true;
            this._providerGrid._selectRow = -1;
            this._providerGrid.BackColor = System.Drawing.SystemColors.Window;
            this._providerGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._providerGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._providerGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._providerGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._providerGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._providerGrid.Location = new System.Drawing.Point(0, 70);
            this._providerGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._providerGrid.Name = "_providerGrid";
            this._providerGrid.Size = new System.Drawing.Size(617, 254);
            this._providerGrid.TabIndex = 4;
            this._providerGrid.TabStop = false;
            // 
            // _buttonLoginAdmin
            // 
            this._buttonLoginAdmin._drawNewMethod = false;
            this._buttonLoginAdmin.AutoSize = true;
            this._buttonLoginAdmin.BackColor = System.Drawing.Color.Transparent;
            this._buttonLoginAdmin.ButtonText = "login";
            this._buttonLoginAdmin.Location = new System.Drawing.Point(524, 35);
            this._buttonLoginAdmin.Margin = new System.Windows.Forms.Padding(2);
            this._buttonLoginAdmin.myImage = global::MyLib.Resource16x16.key1;
            this._buttonLoginAdmin.myTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._buttonLoginAdmin.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonLoginAdmin.myUseVisualStyleBackColor = false;
            this._buttonLoginAdmin.Name = "_buttonLoginAdmin";
            this._buttonLoginAdmin.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this._buttonLoginAdmin.ResourceName = "login";
            this._buttonLoginAdmin.Size = new System.Drawing.Size(59, 24);
            this._buttonLoginAdmin.TabIndex = 3;
            this._buttonLoginAdmin.Text = "login";
            this._buttonLoginAdmin.UseVisualStyleBackColor = false;
            this._buttonLoginAdmin.Click += new System.EventHandler(this._buttonLoginAdmin_Click);
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
            this._passwordTextBox.Location = new System.Drawing.Point(104, 36);
            this._passwordTextBox.Margin = new System.Windows.Forms.Padding(0);
            this._passwordTextBox.MaxLength = 0;
            this._passwordTextBox.Name = "_passwordTextBox";
            this._passwordTextBox.ShowIcon = false;
            this._passwordTextBox.Size = new System.Drawing.Size(418, 22);
            this._passwordTextBox.TabIndex = 2;
            // 
            // _myLabel1
            // 
            this._myLabel1.AutoSize = true;
            this._myLabel1.Location = new System.Drawing.Point(6, 42);
            this._myLabel1.Name = "_myLabel1";
            this._myLabel1.ResourceName = "server_password";
            this._myLabel1.Size = new System.Drawing.Size(90, 13);
            this._myLabel1.TabIndex = 0;
            this._myLabel1.Text = "server_password";
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
            this.panel1.Size = new System.Drawing.Size(617, 70);
            this.panel1.TabIndex = 11;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this._saveButton);
            this.flowLayoutPanel1.Controls.Add(this._closeButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 324);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(617, 29);
            this.flowLayoutPanel1.TabIndex = 12;
            // 
            // _providerConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.ClientSize = new System.Drawing.Size(617, 353);
            this.Controls.Add(this._providerGrid);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_providerConfig";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Provider";
            this.Load += new System.EventHandler(this._providerConfig_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private _myLabel _myLabel1;
        private _myTextBox _passwordTextBox;
        private _myButton _buttonLoginAdmin;
        private _myGrid _providerGrid;
        private _myButton _closeButton;
        private _myButton _saveButton;
        private _myTextBox _webserviceTextBox;
        private _myLabel _webserviceUrl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}