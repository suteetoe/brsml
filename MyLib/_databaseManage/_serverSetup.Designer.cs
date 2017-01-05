namespace MyLib._databaseManage
{
    partial class _serverSetup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_serverSetup));
            this.progressBarShow = new System.Windows.Forms.ProgressBar();
            this.progressText = new System.Windows.Forms.Label();
            this.databaseScreen = new MyLib._myScreen();
            this._buttonConnect = new MyLib._myButton();
            this.SuspendLayout();
            // 
            // progressBarShow
            // 
            this.progressBarShow.Location = new System.Drawing.Point(3, 173);
            this.progressBarShow.Name = "progressBarShow";
            this.progressBarShow.Size = new System.Drawing.Size(387, 23);
            this.progressBarShow.TabIndex = 5;
            // 
            // progressText
            // 
            this.progressText.BackColor = System.Drawing.Color.Transparent;
            this.progressText.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.progressText.ForeColor = System.Drawing.Color.Blue;
            this.progressText.Location = new System.Drawing.Point(3, 199);
            this.progressText.Name = "progressText";
            this.progressText.Size = new System.Drawing.Size(387, 21);
            this.progressText.TabIndex = 6;
            // 
            // databaseScreen
            // 
            this.databaseScreen._isChange = false;
            this.databaseScreen.BackColor = System.Drawing.Color.Transparent;
            this.databaseScreen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.databaseScreen.ForeColor = System.Drawing.Color.Black;
            this.databaseScreen.Location = new System.Drawing.Point(3, 3);
            this.databaseScreen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.databaseScreen.Name = "databaseScreen";
            this.databaseScreen.Size = new System.Drawing.Size(387, 127);
            this.databaseScreen.TabIndex = 0;
            // 
            // _buttonConnect
            // 
            this._buttonConnect.AutoSize = true;
            this._buttonConnect.BackColor = System.Drawing.Color.Transparent;
            this._buttonConnect.ButtonText = "เชื่อมต่อ";
            this._buttonConnect.Location = new System.Drawing.Point(313, 142);
            this._buttonConnect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._buttonConnect.myImage = global::MyLib.Resource16x16.flash;
            this._buttonConnect.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonConnect.myUseVisualStyleBackColor = false;
            this._buttonConnect.Name = "_buttonConnect";
            this._buttonConnect.ResourceName = "connect";
            this._buttonConnect.Size = new System.Drawing.Size(77, 24);
            this._buttonConnect.TabIndex = 1;
            this._buttonConnect.UseVisualStyleBackColor = false;
            this._buttonConnect.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // _serverSetup
            // 
            this._colorBegin = System.Drawing.Color.White;
            this._colorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(394, 227);
            this.Controls.Add(this._buttonConnect);
            this.Controls.Add(this.progressText);
            this.Controls.Add(this.progressBarShow);
            this.Controls.Add(this.databaseScreen);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_serverSetup";
            this.Padding = new System.Windows.Forms.Padding(10, 11, 10, 11);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResourceName = "connect_server";
            this.Load += new System.EventHandler(this._serverSetup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarShow;
        private System.Windows.Forms.Label progressText;
        private _myScreen databaseScreen;
        private _myButton _buttonConnect;

    }
}
