namespace SMLERPSO
{
    partial class _soInvoiceCenter
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
            this._screenSummary = new MyLib._myScreen();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._myToolStrip1 = new MyLib._myToolStrip();
            this._gridTrans = new MyLib._myGrid();
            this._gridDetail = new MyLib._myGrid();
            this._processButton = new System.Windows.Forms.ToolStripButton();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this._myPanel1 = new MyLib._myPanel();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this._myToolStrip1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _screenSummary
            // 
            this._screenSummary._isChange = false;
            this._screenSummary.BackColor = System.Drawing.Color.Transparent;
            this._screenSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenSummary.Location = new System.Drawing.Point(0, 0);
            this._screenSummary.Name = "_screenSummary";
            this._screenSummary.Size = new System.Drawing.Size(782, 782);
            this._screenSummary.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 117);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._gridTrans);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._gridDetail);
            this.splitContainer1.Size = new System.Drawing.Size(782, 665);
            this.splitContainer1.SplitterDistance = 385;
            this.splitContainer1.TabIndex = 3;
            // 
            // _myToolStrip1
            // 
            this._myToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._processButton,
            this._closeButton});
            this._myToolStrip1.Location = new System.Drawing.Point(0, 92);
            this._myToolStrip1.Name = "_myToolStrip1";
            this._myToolStrip1.Size = new System.Drawing.Size(782, 25);
            this._myToolStrip1.TabIndex = 0;
            this._myToolStrip1.Text = "_myToolStrip1";
            // 
            // _gridTrans
            // 
            this._gridTrans._extraWordShow = true;
            this._gridTrans._selectRow = -1;
            this._gridTrans.BackColor = System.Drawing.SystemColors.Window;
            this._gridTrans.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridTrans.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridTrans.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridTrans.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridTrans.Location = new System.Drawing.Point(0, 0);
            this._gridTrans.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridTrans.Name = "_gridTrans";
            this._gridTrans.Size = new System.Drawing.Size(782, 385);
            this._gridTrans.TabIndex = 0;
            this._gridTrans.TabStop = false;
            // 
            // _gridDetail
            // 
            this._gridDetail._extraWordShow = true;
            this._gridDetail._selectRow = -1;
            this._gridDetail.BackColor = System.Drawing.SystemColors.Window;
            this._gridDetail.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridDetail.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridDetail.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridDetail.Location = new System.Drawing.Point(0, 0);
            this._gridDetail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridDetail.Name = "_gridDetail";
            this._gridDetail.Size = new System.Drawing.Size(782, 276);
            this._gridDetail.TabIndex = 1;
            this._gridDetail.TabStop = false;
            // 
            // _processButton
            // 
            this._processButton.Image = global::SMLERPSO.Properties.Resources.flash;
            this._processButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(67, 22);
            this._processButton.Text = "Process";
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPSO.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(56, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._screenSummary);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(782, 92);
            this._myPanel1.TabIndex = 4;
            // 
            // _soInvoiceCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this._myToolStrip1);
            this.Controls.Add(this._myPanel1);
            this.Name = "_soInvoiceCenter";
            this.Size = new System.Drawing.Size(782, 782);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this._myToolStrip1.ResumeLayout(false);
            this._myToolStrip1.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private MyLib._myGrid _gridTrans;
        private MyLib._myToolStrip _myToolStrip1;
        private MyLib._myGrid _gridDetail;
        private System.Windows.Forms.ToolStripButton _processButton;
        private System.Windows.Forms.ToolStripButton _closeButton;
        private MyLib._myPanel _myPanel1;
        public MyLib._myScreen _screenSummary;

    }
}
