namespace SMLInventoryControl
{
    partial class _colorInfo
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_colorInfo));
            this._myManageDetail = new MyLib._myManageData();
            this._icmainScreenTop = new SMLERPControl._icmainScreenTopControl();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._processButton = new System.Windows.Forms.ToolStripButton();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this._icTransItemGrid = new SMLInventoryControl._icTransItemGridControl();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this._qtyTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._myManageDetail._form2.SuspendLayout();
            this._myManageDetail.SuspendLayout();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myManageDetail
            // 
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
            this._myManageDetail._form2.Controls.Add(this._icTransItemGrid);
            this._myManageDetail._form2.Controls.Add(this._toolStrip);
            this._myManageDetail._form2.Controls.Add(this._icmainScreenTop);
            this._myManageDetail.Size = new System.Drawing.Size(814, 574);
            this._myManageDetail.TabIndex = 0;
            this._myManageDetail.TabStop = false;
            // 
            // _icmainScreenTop
            // 
            this._icmainScreenTop._isChange = false;
            this._icmainScreenTop.AutoSize = true;
            this._icmainScreenTop.BackColor = System.Drawing.Color.Transparent;
            this._icmainScreenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._icmainScreenTop.Enabled = false;
            this._icmainScreenTop.Location = new System.Drawing.Point(0, 0);
            this._icmainScreenTop.Name = "_icmainScreenTop";
            this._icmainScreenTop.Size = new System.Drawing.Size(626, 230);
            this._icmainScreenTop.TabIndex = 0;
            // 
            // _toolStrip
            // 
            this._toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this._qtyTextBox,
            this.toolStripSeparator1,
            this._processButton,
            this._closeButton});
            this._toolStrip.Location = new System.Drawing.Point(0, 230);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(626, 25);
            this._toolStrip.TabIndex = 5;
            this._toolStrip.Text = "toolStrip1";
            // 
            // _processButton
            // 
            this._processButton.Image = ((System.Drawing.Image)(resources.GetObject("_processButton.Image")));
            this._processButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(67, 22);
            this._processButton.Text = "Process";
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Image = ((System.Drawing.Image)(resources.GetObject("_closeButton.Image")));
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(56, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _icTransItemGrid
            // 
            this._icTransItemGrid._custCode = "";
            this._icTransItemGrid._extraWordShow = true;
            this._icTransItemGrid._icTransControlType = _g.g._transControlTypeEnum.แสดงราคาตามสูตรสี;
            this._icTransItemGrid._icTransRef = null;
            this._icTransItemGrid._selectRow = -1;
            this._icTransItemGrid.BackColor = System.Drawing.SystemColors.Window;
            this._icTransItemGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._icTransItemGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._icTransItemGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icTransItemGrid.Enabled = false;
            this._icTransItemGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._icTransItemGrid.Location = new System.Drawing.Point(0, 255);
            this._icTransItemGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._icTransItemGrid.Name = "_icTransItemGrid";
            this._icTransItemGrid.Size = new System.Drawing.Size(626, 317);
            this._icTransItemGrid.TabIndex = 6;
            this._icTransItemGrid.TabStop = false;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(26, 22);
            this.toolStripLabel1.Text = "Qty";
            // 
            // _qtyTextBox
            // 
            this._qtyTextBox.Name = "_qtyTextBox";
            this._qtyTextBox.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _colorInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageDetail);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_colorInfo";
            this.Size = new System.Drawing.Size(814, 574);
            this._myManageDetail._form2.ResumeLayout(false);
            this._myManageDetail._form2.PerformLayout();
            this._myManageDetail.ResumeLayout(false);
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myManageData _myManageDetail;
        private SMLERPControl._icmainScreenTopControl _icmainScreenTop;
        private _icTransItemGridControl _icTransItemGrid;
        public System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.ToolStripButton _processButton;
        private System.Windows.Forms.ToolStripButton _closeButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox _qtyTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
