namespace SMLERPControl._customer
{
    partial class _ar_point_balance
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
            this._myToolbar = new System.Windows.Forms.ToolStrip();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._myPanel1 = new MyLib._myPanel();
            this._grid_ar_point_movement1 = new SMLERPControl._customer._grid_ar_point_movement();
            this._screenTop = new SMLERPControl._customer._screen_ar_main();
            this._myManageData1 = new MyLib._myManageData();
            this._myToolbar.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myToolbar
            // 
            this._myToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._closeButton});
            this._myToolbar.Location = new System.Drawing.Point(0, 0);
            this._myToolbar.Name = "_myToolbar";
            this._myToolbar.Size = new System.Drawing.Size(697, 25);
            this._myToolbar.TabIndex = 0;
            this._myToolbar.Text = "toolStrip1";
            // 
            // toolStripMyButton1
            // 
            this._closeButton.Image = global::SMLERPControl.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "toolStripMyButton1";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "";
            this._closeButton.Size = new System.Drawing.Size(55, 22);
            this._closeButton.Text = "ปิดจอ";
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
            this._myManageData1.Size = new System.Drawing.Size(782, 747);
            this._myManageData1.TabIndex = 1;
            this._myManageData1.TabStop = false;
            // 
            // _myManageData1.Panel1
            // 
            this._myManageData1._form1.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // _myManageData1.Panel2
            // 
            this._myManageData1._form2.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData1._form2.Controls.Add(this._myPanel1);
            this._myManageData1._form2.Controls.Add(this._myToolbar);
            this._myManageData1.Size = new System.Drawing.Size(727, 613);
            this._myManageData1.TabIndex = 0;
            this._myManageData1.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._grid_ar_point_movement1);
            this._myPanel1.Controls.Add(this._screenTop);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(697, 701);
            this._myPanel1.TabIndex = 1;
            // 
            // _grid_ar_point_movement1
            // 
            this._grid_ar_point_movement1._extraWordShow = true;
            this._grid_ar_point_movement1._selectRow = -1;
            this._grid_ar_point_movement1.BackColor = System.Drawing.SystemColors.Window;
            this._grid_ar_point_movement1.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._grid_ar_point_movement1.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._grid_ar_point_movement1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid_ar_point_movement1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._grid_ar_point_movement1.Location = new System.Drawing.Point(0, 299);
            this._grid_ar_point_movement1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._grid_ar_point_movement1.Name = "_grid_ar_point_movement1";
            this._grid_ar_point_movement1.Size = new System.Drawing.Size(697, 402);
            this._grid_ar_point_movement1.TabIndex = 1;
            this._grid_ar_point_movement1.TabStop = false;
            // 
            // _screen_ar_main1
            // 
            this._screenTop._controlName = SMLERPControl._customer._controlTypeEnum.Ar;
            this._screenTop._isChange = false;
            this._screenTop.AutoSize = true;
            this._screenTop.BackColor = System.Drawing.Color.Transparent;
            this._screenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenTop.Location = new System.Drawing.Point(0, 0);
            this._screenTop.Name = "_screen_ar_main1";
            this._screenTop.Size = new System.Drawing.Size(697, 299);
            this._screenTop.TabIndex = 0;
            // 
            // _ar_point_balance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData1);
            this.Name = "_ar_point_balance";
            this.Size = new System.Drawing.Size(782, 747);
            this._myToolbar.ResumeLayout(false);
            this._myToolbar.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip _myToolbar;
        public MyLib.ToolStripMyButton _closeButton;
        private MyLib._myPanel _myPanel1;
        private _screen_ar_main _screenTop;
        private _grid_ar_point_movement _grid_ar_point_movement1;
        public MyLib._myManageData _myManageData1;
    }
}
