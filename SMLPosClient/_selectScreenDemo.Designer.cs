namespace SMLPosClient
{
    partial class _selectScreenDemo
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
            this._screenMoniter = new MyLib._myScreen();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._myShadowLabel1 = new MyLib._myShadowLabel(this.components);
            this._myFlowLayoutPanel2 = new MyLib._myFlowLayoutPanel();
            this._buttonSaveConfig = new MyLib.VistaButton();
            this._myPanel1.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this._myFlowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._screenMoniter);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel2);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(335, 251);
            this._myPanel1.TabIndex = 1;
            // 
            // _screenMoniter
            // 
            this._screenMoniter._isChange = false;
            this._screenMoniter.BackColor = System.Drawing.Color.Transparent;
            this._screenMoniter.Dock = System.Windows.Forms.DockStyle.Fill;
            this._screenMoniter.Location = new System.Drawing.Point(0, 43);
            this._screenMoniter.Name = "_screenMoniter";
            this._screenMoniter.Size = new System.Drawing.Size(335, 169);
            this._screenMoniter.TabIndex = 2;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._myShadowLabel1);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(335, 43);
            this._myFlowLayoutPanel1.TabIndex = 1;
            // 
            // _myShadowLabel1
            // 
            this._myShadowLabel1.Angle = 0F;
            this._myShadowLabel1.AutoSize = true;
            this._myShadowLabel1.EndColor = System.Drawing.Color.Transparent;
            this._myShadowLabel1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myShadowLabel1.ForeColor = System.Drawing.Color.LightSkyBlue;
            this._myShadowLabel1.Location = new System.Drawing.Point(142, 5);
            this._myShadowLabel1.Name = "_myShadowLabel1";
            this._myShadowLabel1.ShadowColor = System.Drawing.Color.Black;
            this._myShadowLabel1.Size = new System.Drawing.Size(190, 29);
            this._myShadowLabel1.StartColor = System.Drawing.Color.Transparent;
            this._myShadowLabel1.TabIndex = 0;
            this._myShadowLabel1.Text = "เลือกจอแสดงผล";
            this._myShadowLabel1.XOffset = 1F;
            this._myShadowLabel1.YOffset = 1F;
            // 
            // _myFlowLayoutPanel2
            // 
            this._myFlowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel2.Controls.Add(this._buttonSaveConfig);
            this._myFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel2.Location = new System.Drawing.Point(0, 212);
            this._myFlowLayoutPanel2.Name = "_myFlowLayoutPanel2";
            this._myFlowLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this._myFlowLayoutPanel2.Size = new System.Drawing.Size(335, 39);
            this._myFlowLayoutPanel2.TabIndex = 1;
            // 
            // _buttonSaveConfig
            // 
            this._buttonSaveConfig.BackColor = System.Drawing.Color.Transparent;
            this._buttonSaveConfig.ButtonText = "GO !!";
            this._buttonSaveConfig.Location = new System.Drawing.Point(251, 8);
            this._buttonSaveConfig.myImage = global::SMLPosClient.Properties.Resources.disk_blue;
            this._buttonSaveConfig.Name = "_buttonSaveConfig";
            this._buttonSaveConfig.Size = new System.Drawing.Size(81, 25);
            this._buttonSaveConfig.TabIndex = 1;
            this._buttonSaveConfig.Text = "vistaButton2";
            this._buttonSaveConfig.UseVisualStyleBackColor = false;
            this._buttonSaveConfig.Click += new System.EventHandler(this._buttonSaveConfig_Click);
            // 
            // _selectScreenDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 251);
            this.ControlBox = false;
            this.Controls.Add(this._myPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_selectScreenDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "_selectScreenDemo";
            this._myPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this._myFlowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private MyLib._myScreen _screenMoniter;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib._myShadowLabel _myShadowLabel1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel2;
        private MyLib.VistaButton _buttonSaveConfig;
    }
}