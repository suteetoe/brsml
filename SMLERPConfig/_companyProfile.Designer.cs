namespace SMLERPConfig
{
	partial class _companyProfile
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
            this._toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonSave = new MyLib.ToolStripMyButton();
            this._buttonClose = new MyLib.ToolStripMyButton();
            this._myPanel1 = new MyLib._myPanel();
            this._myTabControl1 = new MyLib._myTabControl();
            this.tab_companyprofile = new System.Windows.Forms.TabPage();
            this.tab_companydetail = new System.Windows.Forms.TabPage();
            this._flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._myShadowLabel1 = new MyLib._myShadowLabel(this.components);
            this._companyProfileScreen1 = new SMLERPConfig._companyProfileScreen();
            this._companyProfileDetailScreen1 = new SMLERPConfig._companyProfileDetailScreen();
            this._toolStrip1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myTabControl1.SuspendLayout();
            this.tab_companyprofile.SuspendLayout();
            this.tab_companydetail.SuspendLayout();
            this._flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolStrip1
            // 
            this._toolStrip1.BackgroundImage = global::SMLERPConfig.Resource16x16.bt03;
            this._toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSave,
            this._buttonClose});
            this._toolStrip1.Location = new System.Drawing.Point(2, 2);
            this._toolStrip1.Name = "_toolStrip1";
            this._toolStrip1.Size = new System.Drawing.Size(731, 25);
            this._toolStrip1.TabIndex = 7;
            // 
            // _buttonSave
            // 
            this._buttonSave.Image = global::SMLERPConfig.Resource16x16.disk_blue;
            this._buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSave.ResourceName = "บันทึกข้อมูล (F12)";
            this._buttonSave.Size = new System.Drawing.Size(122, 22);
            this._buttonSave.Text = "บันทึกข้อมูล (F12)";
            this._buttonSave.Click += new System.EventHandler(this._buttonSave_Click);
            // 
            // _buttonClose
            // 
            this._buttonClose.Image = global::SMLERPConfig.Resource16x16.error;
            this._buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Padding = new System.Windows.Forms.Padding(1);
            this._buttonClose.ResourceName = "ปิดหน้าจอ";
            this._buttonClose.Size = new System.Drawing.Size(79, 22);
            this._buttonClose.Text = "ปิดหน้าจอ";
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.AutoSize = true;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._myTabControl1);
            this._myPanel1.Controls.Add(this._flowLayoutPanel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(2, 27);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.ShowLineBackground = true;
            this._myPanel1.Size = new System.Drawing.Size(731, 583);
            this._myPanel1.TabIndex = 8;
            // 
            // _myTabControl1
            // 
            this._myTabControl1.Controls.Add(this.tab_companyprofile);
            this._myTabControl1.Controls.Add(this.tab_companydetail);
            this._myTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTabControl1.Font = new System.Drawing.Font("Tahoma", 9F);
            this._myTabControl1.Location = new System.Drawing.Point(0, 43);
            this._myTabControl1.Multiline = true;
            this._myTabControl1.Name = "_myTabControl1";
            this._myTabControl1.SelectedIndex = 0;
            this._myTabControl1.Size = new System.Drawing.Size(731, 540);
            this._myTabControl1.TabIndex = 10;
            this._myTabControl1.TableName = "";
            // 
            // tab_companyprofile
            // 
            this.tab_companyprofile.Controls.Add(this._companyProfileScreen1);
            this.tab_companyprofile.Location = new System.Drawing.Point(4, 23);
            this.tab_companyprofile.Name = "tab_companyprofile";
            this.tab_companyprofile.Padding = new System.Windows.Forms.Padding(3);
            this.tab_companyprofile.Size = new System.Drawing.Size(723, 513);
            this.tab_companyprofile.TabIndex = 0;
            this.tab_companyprofile.Text = "1.tab_companyprofile";
            this.tab_companyprofile.UseVisualStyleBackColor = true;
            // 
            // tab_companydetail
            // 
            this.tab_companydetail.Controls.Add(this._companyProfileDetailScreen1);
            this.tab_companydetail.Location = new System.Drawing.Point(4, 23);
            this.tab_companydetail.Name = "tab_companydetail";
            this.tab_companydetail.Padding = new System.Windows.Forms.Padding(3);
            this.tab_companydetail.Size = new System.Drawing.Size(723, 503);
            this.tab_companydetail.TabIndex = 1;
            this.tab_companydetail.Text = "2.tab_profiledetail";
            this.tab_companydetail.UseVisualStyleBackColor = true;
            // 
            // _flowLayoutPanel1
            // 
            this._flowLayoutPanel1.AutoSize = true;
            this._flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._flowLayoutPanel1.Controls.Add(this._myShadowLabel1);
            this._flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this._flowLayoutPanel1.Name = "_flowLayoutPanel1";
            this._flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this._flowLayoutPanel1.Size = new System.Drawing.Size(731, 43);
            this._flowLayoutPanel1.TabIndex = 9;
            // 
            // _myShadowLabel1
            // 
            this._myShadowLabel1.Angle = 0F;
            this._myShadowLabel1.AutoSize = true;
            this._myShadowLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myShadowLabel1.DrawGradient = false;
            this._myShadowLabel1.EndColor = System.Drawing.Color.LightSkyBlue;
            this._myShadowLabel1.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myShadowLabel1.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this._myShadowLabel1.Location = new System.Drawing.Point(467, 5);
            this._myShadowLabel1.Name = "_myShadowLabel1";
            this._myShadowLabel1.ResourceName = "";
            this._myShadowLabel1.ShadowColor = System.Drawing.Color.Gray;
            this._myShadowLabel1.Size = new System.Drawing.Size(261, 33);
            this._myShadowLabel1.StartColor = System.Drawing.Color.White;
            this._myShadowLabel1.TabIndex = 7;
            this._myShadowLabel1.Text = "Company Profiles.";
            this._myShadowLabel1.XOffset = 1F;
            this._myShadowLabel1.YOffset = 1F;
            // 
            // _companyProfileScreen1
            // 
            this._companyProfileScreen1._isChange = false;
            this._companyProfileScreen1.AutoSize = true;
            this._companyProfileScreen1.BackColor = System.Drawing.Color.Transparent;
            this._companyProfileScreen1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._companyProfileScreen1.Location = new System.Drawing.Point(3, 3);
            this._companyProfileScreen1.Name = "_companyProfileScreen1";
            this._companyProfileScreen1.Padding = new System.Windows.Forms.Padding(5);
            this._companyProfileScreen1.Size = new System.Drawing.Size(717, 507);
            this._companyProfileScreen1.TabIndex = 0;
            // 
            // _companyProfileDetailScreen1
            // 
            this._companyProfileDetailScreen1._isChange = false;
            this._companyProfileDetailScreen1.BackColor = System.Drawing.Color.Transparent;
            this._companyProfileDetailScreen1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._companyProfileDetailScreen1.Location = new System.Drawing.Point(3, 3);
            this._companyProfileDetailScreen1.Name = "_companyProfileDetailScreen1";
            this._companyProfileDetailScreen1.Size = new System.Drawing.Size(717, 497);
            this._companyProfileDetailScreen1.TabIndex = 0;
            // 
            // _companyProfile
            // 
            this._colorBackground = false;
            this._colorBegin = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(735, 612);
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this._toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "_companyProfile";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_companyProfile";
            this.Load += new System.EventHandler(this._companyProfile_Load);
            this._toolStrip1.ResumeLayout(false);
            this._toolStrip1.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myTabControl1.ResumeLayout(false);
            this.tab_companyprofile.ResumeLayout(false);
            this.tab_companyprofile.PerformLayout();
            this.tab_companydetail.ResumeLayout(false);
            this._flowLayoutPanel1.ResumeLayout(false);
            this._flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ToolStrip _toolStrip1;
        private MyLib.ToolStripMyButton _buttonSave;
        private MyLib.ToolStripMyButton _buttonClose;
        private MyLib._myPanel _myPanel1;
        private MyLib._myShadowLabel _myShadowLabel1;
        private System.Windows.Forms.FlowLayoutPanel _flowLayoutPanel1;
        private _companyProfileScreen _companyProfileScreen1;
        private MyLib._myTabControl _myTabControl1;
        private System.Windows.Forms.TabPage tab_companyprofile;
        private System.Windows.Forms.TabPage tab_companydetail;
        private _companyProfileDetailScreen _companyProfileDetailScreen1;
	}
}