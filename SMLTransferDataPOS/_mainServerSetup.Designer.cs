namespace SMLTransferDataPOS
{
    partial class _mainServerSetup
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
            this._myPanel1 = new MyLib._myPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this._screen_server_config = new MyLib._myScreen();
            this._myFlowLayoutPanel2 = new MyLib._myFlowLayoutPanel();
            this._myShadowLabel1 = new MyLib._myShadowLabel(this.components);
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._saveButton = new MyLib.VistaButton();
            this._closeButton = new MyLib.VistaButton();
            this._myPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this._myFlowLayoutPanel2.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this.panel1);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel2);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.ShowLineBackground = true;
            this._myPanel1.Size = new System.Drawing.Size(681, 447);
            this._myPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this._screen_server_config);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(681, 354);
            this.panel1.TabIndex = 2;
            // 
            // _screen_server_config
            // 
            this._screen_server_config._isChange = false;
            this._screen_server_config.BackColor = System.Drawing.Color.Transparent;
            this._screen_server_config.Dock = System.Windows.Forms.DockStyle.Fill;
            this._screen_server_config.Location = new System.Drawing.Point(0, 0);
            this._screen_server_config.Name = "_screen_server_config";
            this._screen_server_config.Size = new System.Drawing.Size(681, 354);
            this._screen_server_config.TabIndex = 0;
            // 
            // _myFlowLayoutPanel2
            // 
            this._myFlowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel2.Controls.Add(this._myShadowLabel1);
            this._myFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this._myFlowLayoutPanel2.Name = "_myFlowLayoutPanel2";
            this._myFlowLayoutPanel2.Padding = new System.Windows.Forms.Padding(5);
            this._myFlowLayoutPanel2.Size = new System.Drawing.Size(681, 58);
            this._myFlowLayoutPanel2.TabIndex = 1;
            // 
            // _myShadowLabel1
            // 
            this._myShadowLabel1.Angle = 0F;
            this._myShadowLabel1.AutoSize = true;
            this._myShadowLabel1.EndColor = System.Drawing.Color.Transparent;
            this._myShadowLabel1.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myShadowLabel1.ForeColor = System.Drawing.Color.LightSkyBlue;
            this._myShadowLabel1.Location = new System.Drawing.Point(263, 5);
            this._myShadowLabel1.Name = "_myShadowLabel1";
            this._myShadowLabel1.ShadowColor = System.Drawing.Color.Black;
            this._myShadowLabel1.Size = new System.Drawing.Size(405, 45);
            this._myShadowLabel1.StartColor = System.Drawing.Color.Transparent;
            this._myShadowLabel1.TabIndex = 0;
            this._myShadowLabel1.Text = "กำหนดค่าเครื่องแม่ข่าย";
            this._myShadowLabel1.XOffset = 1F;
            this._myShadowLabel1.YOffset = 1F;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._saveButton);
            this._myFlowLayoutPanel1.Controls.Add(this._closeButton);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 412);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Padding = new System.Windows.Forms.Padding(2);
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(681, 35);
            this._myFlowLayoutPanel1.TabIndex = 0;
            // 
            // _saveButton
            // 
            this._saveButton._drawNewMethod = false;
            this._saveButton.BackColor = System.Drawing.Color.Transparent;
            this._saveButton.ButtonText = "บันทึก";
            this._saveButton.Location = new System.Drawing.Point(594, 5);
            this._saveButton.myImage = global::SMLTransferDataPOS.Properties.Resources.disk_blue;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(80, 26);
            this._saveButton.TabIndex = 0;
            this._saveButton.Text = "vistaButton1";
            this._saveButton.UseVisualStyleBackColor = false;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton._drawNewMethod = false;
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.ButtonText = "ปิดจอ";
            this._closeButton.Location = new System.Drawing.Point(508, 5);
            this._closeButton.myImage = global::SMLTransferDataPOS.Properties.Resources.error;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(80, 26);
            this._closeButton.TabIndex = 1;
            this._closeButton.Text = "vistaButton2";
            this._closeButton.UseVisualStyleBackColor = false;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _mainServerSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 447);
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_mainServerSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "กำหนดค่าเครื่องแม่ข่าย";
            this._myPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this._myFlowLayoutPanel2.ResumeLayout(false);
            this._myFlowLayoutPanel2.PerformLayout();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel2;
        private MyLib._myShadowLabel _myShadowLabel1;
        private MyLib.VistaButton _saveButton;
        private System.Windows.Forms.Panel panel1;
        private MyLib.VistaButton _closeButton;
        private MyLib._myScreen _screen_server_config;
    }
}