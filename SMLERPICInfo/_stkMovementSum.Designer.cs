namespace SMLERPICInfo
{
    partial class _stkMovementSum
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_stkMovementSum));
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this._conditionScreen = new SMLERPICInfo._stkConditionScreen();
            this._processButton = new System.Windows.Forms.ToolStripButton();
            this._resultGrid = new MyLib._myGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
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
            // _conditionScreen
            // 
            this._conditionScreen._isChange = false;
            this._conditionScreen._stkConditionType = SMLERPICInfo._stkConditionControlEnum.สรุปเคลื่อนไหวสินค้าตามปริมาณ;
            this._conditionScreen.AutoSize = true;
            this._conditionScreen.BackColor = System.Drawing.Color.Transparent;
            this._conditionScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this._conditionScreen.Location = new System.Drawing.Point(0, 0);
            this._conditionScreen.Name = "_conditionScreen";
            this._conditionScreen.Size = new System.Drawing.Size(685, 46);
            this._conditionScreen.TabIndex = 10;
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
            // _resultGrid
            // 
            this._resultGrid._extraWordShow = true;
            this._resultGrid._selectRow = -1;
            this._resultGrid.BackColor = System.Drawing.SystemColors.Window;
            this._resultGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._resultGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._resultGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._resultGrid.IsEdit = false;
            this._resultGrid.Location = new System.Drawing.Point(0, 71);
            this._resultGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._resultGrid.Name = "_resultGrid";
            this._resultGrid.Size = new System.Drawing.Size(685, 461);
            this._resultGrid.TabIndex = 8;
            this._resultGrid.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._processButton,
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 46);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(685, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _stkMovementSum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._resultGrid);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this._conditionScreen);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_stkMovementSum";
            this.Size = new System.Drawing.Size(685, 532);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton _closeButton;
        private _stkConditionScreen _conditionScreen;
        private System.Windows.Forms.ToolStripButton _processButton;
        private MyLib._myGrid _resultGrid;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}
