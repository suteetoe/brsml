namespace SMLERPIC
{
	partial class _icMain
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
            this._myManageMain.Size = new System.Drawing.Size(1218, 732);
            this._myManageMain.TabIndex = 0;
            this._myManageMain.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = true;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(3);
            this._myPanel1.Size = new System.Drawing.Size(936, 705);
            this._myPanel1.TabIndex = 1;
            // 
            // _myToolBar
            // 
            this._myToolBar.BackgroundImage = global::SMLERPIC.Properties.Resources.bt03;
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(936, 25);
            this._myToolBar.TabIndex = 0;
            this._myToolBar.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPIC.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึกข้อมูล (F12)";
            this._saveButton.Size = new System.Drawing.Size(113, 22);
            this._saveButton.Text = "บันทึกข้อมูล (F12)";
            // 
            // _icMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageMain);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icMain";
            this.Size = new System.Drawing.Size(1218, 732);
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
        private MyLib._myPanel _myPanel1;
        private MyLib.ToolStripMyButton _saveButton;
    }
}
