namespace SMLERPIC
{
    partial class _icSerialNumber
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
            this._myManageMain = new MyLib._myManageData();
            this._myPanel1 = new MyLib._myPanel();
            this._movement = new SMLERPICInfo._serialNumberMovement();
            this._serialNumberScreen = new SMLInventoryControl._icSerialNumerScreen();
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._myManageMain._form2.SuspendLayout();
            this._myManageMain.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myManageMain
            // 
            this._myManageMain._mainMenuCode = "";
            this._myManageMain._mainMenuId = "";
            this._myManageMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageMain.Location = new System.Drawing.Point(0, 0);
            this._myManageMain.Name = "_myManageMain";
            // 
            // _myManageMain.Panel1
            // 
            this._myManageMain._form1.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // _myManageMain.Panel2
            // 
            this._myManageMain._form2.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageMain._form2.Controls.Add(this._myPanel1);
            this._myManageMain._form2.Controls.Add(this._myToolBar);
            this._myManageMain.Size = new System.Drawing.Size(1056, 837);
            this._myManageMain.TabIndex = 0;
            this._myManageMain.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._movement);
            this._myPanel1.Controls.Add(this._serialNumberScreen);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(810, 810);
            this._myPanel1.TabIndex = 1;
            // 
            // _movement
            // 
            this._movement._extraWordShow = true;
            this._movement._selectRow = -1;
            this._movement.BackColor = System.Drawing.SystemColors.Window;
            this._movement.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._movement.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._movement.Dock = System.Windows.Forms.DockStyle.Fill;
            this._movement.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._movement.Location = new System.Drawing.Point(0, 161);
            this._movement.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._movement.Name = "_movement";
            this._movement.Size = new System.Drawing.Size(810, 649);
            this._movement.TabIndex = 3;
            this._movement.TabStop = false;
            // 
            // _serialNumberScreen
            // 
            this._serialNumberScreen._isChange = false;
            this._serialNumberScreen.AutoSize = true;
            this._serialNumberScreen.BackColor = System.Drawing.Color.Transparent;
            this._serialNumberScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this._serialNumberScreen.Location = new System.Drawing.Point(0, 0);
            this._serialNumberScreen.Name = "_serialNumberScreen";
            this._serialNumberScreen.Size = new System.Drawing.Size(810, 161);
            this._serialNumberScreen.TabIndex = 2;
            // 
            // _myToolBar
            // 
            this._myToolBar.BackgroundImage = global::SMLERPIC.Properties.Resources.bt03;
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(810, 25);
            this._myToolBar.TabIndex = 1;
            this._myToolBar.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPIC.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึกข้อมูล (F12)";
            this._saveButton.Size = new System.Drawing.Size(115, 22);
            this._saveButton.Text = "บันทึกข้อมูล (F12)";
            // 
            // _icSerialNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageMain);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icSerialNumber";
            this.Size = new System.Drawing.Size(1056, 837);
            this._myManageMain._form2.ResumeLayout(false);
            this._myManageMain._form2.PerformLayout();
            this._myManageMain.ResumeLayout(false);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myManageData _myManageMain;
        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _saveButton;
        private SMLInventoryControl._icSerialNumerScreen _serialNumberScreen;
        private MyLib._myPanel _myPanel1;
        private SMLERPICInfo._serialNumberMovement _movement;
    }
}
