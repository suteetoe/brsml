namespace SMLERPIC
{
    partial class _icMainProductSet
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
            this._myManageDetail = new MyLib._myManageData();
            this._myPanel1 = new MyLib._myPanel();
            this._icMainSetGrid = new SMLInventoryControl._icTransItemGridControl();
            this._icmainScreenTop = new SMLERPControl._icmainScreenTopControl();
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._myManageDetail._form2.SuspendLayout();
            this._myManageDetail.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myManageDetail
            // 
            this._myManageDetail._mainMenuCode = "";
            this._myManageDetail._mainMenuId = "";
            this._myManageDetail.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageDetail.Location = new System.Drawing.Point(0, 0);
            this._myManageDetail.Name = "_myManageDetail";
            // 
            // _myManageDetail.Panel1
            // 
            this._myManageDetail._form1.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // _myManageDetail.Panel2
            // 
            this._myManageDetail._form2.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageDetail._form2.Controls.Add(this._myPanel1);
            this._myManageDetail._form2.Controls.Add(this._myToolBar);
            this._myManageDetail.Size = new System.Drawing.Size(900, 722);
            this._myManageDetail.TabIndex = 1;
            this._myManageDetail.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = true;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._icMainSetGrid);
            this._myPanel1.Controls.Add(this._icmainScreenTop);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(3);
            this._myPanel1.Size = new System.Drawing.Size(692, 695);
            this._myPanel1.TabIndex = 1;
            // 
            // _icMainSetGrid
            // 
            this._icMainSetGrid._custCode = "";
            this._icMainSetGrid._extraWordShow = true;
            this._icMainSetGrid._icTransControlType = _g.g._transControlTypeEnum.สินค้า_สินค้าจัดชุด;
            this._icMainSetGrid._icTransRef = null;
            this._icMainSetGrid._selectRow = -1;
            this._icMainSetGrid.BackColor = System.Drawing.SystemColors.Window;
            this._icMainSetGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._icMainSetGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._icMainSetGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icMainSetGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._icMainSetGrid.Location = new System.Drawing.Point(3, 282);
            this._icMainSetGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._icMainSetGrid.Name = "_icMainSetGrid";
            this._icMainSetGrid.Size = new System.Drawing.Size(686, 410);
            this._icMainSetGrid.TabIndex = 1;
            this._icMainSetGrid.TabStop = false;
            // 
            // _icmainScreenTop
            // 
            this._icmainScreenTop._isChange = false;
            this._icmainScreenTop.AutoSize = true;
            this._icmainScreenTop.BackColor = System.Drawing.Color.Transparent;
            this._icmainScreenTop.DisplayScreen = false;
            this._icmainScreenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._icmainScreenTop.Location = new System.Drawing.Point(3, 3);
            this._icmainScreenTop.Name = "_icmainScreenTop";
            this._icmainScreenTop.Size = new System.Drawing.Size(686, 279);
            this._icmainScreenTop.TabIndex = 0;
            // 
            // _myToolBar
            // 
            this._myToolBar.BackgroundImage = global::SMLERPIC.Properties.Resources.bt03;
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(692, 25);
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
            // _icMainSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageDetail);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icMainSet";
            this.Size = new System.Drawing.Size(900, 722);
            this._myManageDetail._form2.ResumeLayout(false);
            this._myManageDetail._form2.PerformLayout();
            this._myManageDetail.ResumeLayout(false);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SMLERPControl._icmainScreenTopControl _icmainScreenTop;
        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _saveButton;
        private MyLib._myManageData _myManageDetail;
        private MyLib._myPanel _myPanel1;
        private SMLInventoryControl._icTransItemGridControl _icMainSetGrid;

    }
}
