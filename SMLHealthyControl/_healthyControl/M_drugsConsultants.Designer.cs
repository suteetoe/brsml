namespace SMLHealthyControl._healthyControl
{
    partial class M_drugsConsultants
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(M_drugsConsultants));
            this._myManageData1 = new MyLib._myManageData();
            this._myPanel1 = new MyLib._myPanel();
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._save = new MyLib.ToolStripMyButton();
            this._close = new MyLib.ToolStripMyButton();
            this._healthy_drugsConsultants = new SMLHealthyControl._Control._healthy_drugsConsultants();
            this.__grid_drugsConsultants = new _Control._screen_healthy_grid();
            this._myPanel1.SuspendLayout();
            this._myToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myManageData1
            // 
            this._myManageData1._mainMenuCode = "";
            this._myManageData1._mainMenuId = "";
            this._myManageData1.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageData1.Location = new System.Drawing.Point(0, 0);
            this._myManageData1.Name = "_myManageData1";
            this._myManageData1.Size = new System.Drawing.Size(692, 565);
            this._myManageData1.TabIndex = 0;
            this._myManageData1.TabStop = false;
            this._myManageData1._form2.Controls.Add(this._myPanel1);
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this.__grid_drugsConsultants);
            this._myPanel1.Controls.Add(this._healthy_drugsConsultants);
            
            this._myPanel1.Controls.Add(this._myToolBar);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(601, 544);
            this._myPanel1.TabIndex = 1;
            // 
            // _myToolBar
            // 
            this._myToolBar.BackgroundImage = global::SMLHealthyControl.Properties.Resources.bt03;
            this._myToolBar.GripMargin = new System.Windows.Forms.Padding(5);
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._save,
            this._close});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(601, 25);
            this._myToolBar.TabIndex = 3;
            this._myToolBar.Text = "toolStrip1";
            // 
            // _save
            // 
            this._save.Image = global::SMLHealthyControl.Properties.Resources.disk_blue;
            //this._save.Image = ((System.Drawing.Image)(resources.GetObject("_save.Image")));
            this._save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._save.Name = "_save";
            this._save.Padding = new System.Windows.Forms.Padding(1);
            this._save.ResourceName = "";
            this._save.Size = new System.Drawing.Size(58, 22);
            this._save.Text = "บันทึก (F12)";
            // 
            // toolStripMyButton1
            // 
            this._close.Image = global::SMLHealthyControl.Properties.Resources.error;
            this._close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._close.Name = "toolStripMyButton1";
            this._close.Padding = new System.Windows.Forms.Padding(1);
            this._close.ResourceName = "";
            this._close.Size = new System.Drawing.Size(55, 22);
            this._close.Text = "ปิดจอ";
            // 
            // _healthy_patientProfile1
            // 
            this._healthy_drugsConsultants._isChange = true;
            this._healthy_drugsConsultants.AutoSize = true;
            this._healthy_drugsConsultants.BackColor = System.Drawing.Color.Transparent;
            this._healthy_drugsConsultants.Dock = System.Windows.Forms.DockStyle.Top;
            this._healthy_drugsConsultants.Location = new System.Drawing.Point(0, 25);
            this._healthy_drugsConsultants.Name = "_healthy_drugsConsultants";
            this._healthy_drugsConsultants.Size = new System.Drawing.Size(601, 345);
            this._healthy_drugsConsultants.TabIndex = 0;
            this._healthy_drugsConsultants.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);

            this.__grid_drugsConsultants._controlName = SMLHealthyControl._Control._controlTypeEnumGrid.drugsConsultants_details;
            this.__grid_drugsConsultants._extraWordShow = true;
            this.__grid_drugsConsultants._selectRow = -1;
            this.__grid_drugsConsultants.BackColor = System.Drawing.SystemColors.Window;
            this.__grid_drugsConsultants.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.__grid_drugsConsultants.ColumnBackgroundAuto = false;
            this.__grid_drugsConsultants.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this.__grid_drugsConsultants.Dock = System.Windows.Forms.DockStyle.Fill;
            this.__grid_drugsConsultants.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.__grid_drugsConsultants.Location = new System.Drawing.Point(3, 3);
            this.__grid_drugsConsultants.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.__grid_drugsConsultants.Name = "__grid_drugsConsultants";
            this.__grid_drugsConsultants.Size = new System.Drawing.Size(508, 507);
            this.__grid_drugsConsultants.TabIndex = 0;
            this.__grid_drugsConsultants.TabStop = false;
            // 
            // M_PatientProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData1);
            this.Name = "M_drugsConsultants";
            this.Size = new System.Drawing.Size(692, 565);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.ToolStrip _myToolBar;
        private _Control._healthy_drugsConsultants _healthy_drugsConsultants;
        private _Control._screen_healthy_grid __grid_drugsConsultants;
        private MyLib._myManageData _myManageData1;
        private MyLib.ToolStripMyButton _save;
        private MyLib.ToolStripMyButton _close;





    }
}
