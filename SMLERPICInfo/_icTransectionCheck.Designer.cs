namespace SMLERPICInfo
{
    partial class _icTransectionCheck
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
            this._myGrid = new MyLib._myGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._processButton = new System.Windows.Forms.ToolStripButton();
            this._viewDetailButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._updateButton = new MyLib.VistaButton();
            this._cancelButton = new MyLib.VistaButton();
            this.toolStrip1.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myGrid
            // 
            this._myGrid._extraWordShow = true;
            this._myGrid._selectRow = -1;
            this._myGrid.BackColor = System.Drawing.SystemColors.Window;
            this._myGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._myGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._myGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myGrid.Location = new System.Drawing.Point(0, 25);
            this._myGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._myGrid.Name = "_myGrid";
            this._myGrid.Size = new System.Drawing.Size(690, 391);
            this._myGrid.TabIndex = 0;
            this._myGrid.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._processButton,
            this._viewDetailButton,
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(690, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _processButton
            // 
            this._processButton.Image = global::SMLERPICInfo.Properties.Resources.flash;
            this._processButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(133, 22);
            this._processButton.Text = "ตรวจสอบรายการสินค้า";
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _viewDetailButton
            // 
            this._viewDetailButton.Image = global::SMLERPICInfo.Properties.Resources.document_notebook;
            this._viewDetailButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._viewDetailButton.Name = "_viewDetailButton";
            this._viewDetailButton.Padding = new System.Windows.Forms.Padding(1);
            this._viewDetailButton.ResourceName = "แสดงรายละเอียด";
            this._viewDetailButton.Size = new System.Drawing.Size(109, 22);
            this._viewDetailButton.Text = "แสดงรายละเอียด";
            this._viewDetailButton.Click += new System.EventHandler(this._viewDetailButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPICInfo.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "ปิดจอ";
            this._closeButton.Size = new System.Drawing.Size(55, 22);
            this._closeButton.Text = "ปิดจอ";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._updateButton);
            this._myFlowLayoutPanel1.Controls.Add(this._cancelButton);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 416);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(690, 33);
            this._myFlowLayoutPanel1.TabIndex = 2;
            // 
            // _updateButton
            // 
            this._updateButton._drawNewMethod = false;
            this._updateButton.BackColor = System.Drawing.Color.Transparent;
            this._updateButton.ButtonText = "Update";
            this._updateButton.Location = new System.Drawing.Point(610, 3);
            this._updateButton.Name = "_updateButton";
            this._updateButton.Size = new System.Drawing.Size(77, 28);
            this._updateButton.TabIndex = 0;
            this._updateButton.Text = "vistaButton1";
            this._updateButton.UseVisualStyleBackColor = false;
            this._updateButton.Click += new System.EventHandler(this._updateButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton._drawNewMethod = false;
            this._cancelButton.BackColor = System.Drawing.Color.Transparent;
            this._cancelButton.ButtonText = "Cancel";
            this._cancelButton.Location = new System.Drawing.Point(522, 3);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(82, 28);
            this._cancelButton.TabIndex = 1;
            this._cancelButton.Text = "vistaButton2";
            this._cancelButton.UseVisualStyleBackColor = false;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _icTransectionCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myGrid);
            this.Controls.Add(this._myFlowLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_icTransectionCheck";
            this.Size = new System.Drawing.Size(690, 449);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myGrid _myGrid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _processButton;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib.VistaButton _updateButton;
        private MyLib.VistaButton _cancelButton;
        private MyLib.ToolStripMyButton _closeButton;
        private MyLib.ToolStripMyButton _viewDetailButton;
    }
}
