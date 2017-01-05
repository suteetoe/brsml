namespace SMLERPIC
{
    partial class _icSearchLevelScreen
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
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._saveButton = new System.Windows.Forms.ToolStripButton();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this._panel = new MyLib._myPanel();
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this._grid = new MyLib._myGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._loadItem = new MyLib.ToolStripMyButton();
            this._conditionGrid = new MyLib._myGrid();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this._loadItemCondition = new MyLib.ToolStripMyButton();
            this._toolStrip.SuspendLayout();
            this._panel.SuspendLayout();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolStrip
            // 
            this._toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closeButton});
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(1090, 25);
            this._toolStrip.TabIndex = 1;
            this._toolStrip.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPIC.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(110, 22);
            this._saveButton.Text = "บันทึกข้อมูล (F12)";
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPIC.Properties.Resources.error1;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(72, 22);
            this._closeButton.Text = "ปิดหน้าจอ";
            // 
            // _panel
            // 
            this._panel._switchTabAuto = false;
            this._panel.BackColor = System.Drawing.Color.Transparent;
            this._panel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._panel.Controls.Add(this._splitContainer);
            this._panel.CornerPicture = null;
            this._panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._panel.Location = new System.Drawing.Point(0, 25);
            this._panel.Name = "_panel";
            this._panel.Size = new System.Drawing.Size(1090, 614);
            this._panel.TabIndex = 0;
            // 
            // _splitContainer
            // 
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.Location = new System.Drawing.Point(0, 0);
            this._splitContainer.Name = "_splitContainer";
            this._splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._grid);
            this._splitContainer.Panel1.Controls.Add(this.toolStrip1);
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add(this._conditionGrid);
            this._splitContainer.Panel2.Controls.Add(this.toolStrip2);
            this._splitContainer.Size = new System.Drawing.Size(1090, 614);
            this._splitContainer.SplitterDistance = 293;
            this._splitContainer.TabIndex = 4;
            // 
            // _grid
            // 
            this._grid._extraWordShow = true;
            this._grid._selectRow = -1;
            this._grid.BackColor = System.Drawing.SystemColors.Window;
            this._grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._grid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._grid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._grid.Location = new System.Drawing.Point(0, 25);
            this._grid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._grid.Name = "_grid";
            this._grid.Size = new System.Drawing.Size(1090, 268);
            this._grid.TabIndex = 2;
            this._grid.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._loadItem});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1090, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _loadItem
            // 
            this._loadItem.Image = global::SMLERPIC.Properties.Resources.lightbulb_on;
            this._loadItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._loadItem.Name = "_loadItem";
            this._loadItem.Padding = new System.Windows.Forms.Padding(1);
            this._loadItem.ResourceName = "";
            this._loadItem.Size = new System.Drawing.Size(168, 22);
            this._loadItem.Text = "เรียกรายการหน่วยนับ Barcode";
            // 
            // _conditionGrid
            // 
            this._conditionGrid._extraWordShow = true;
            this._conditionGrid._selectRow = -1;
            this._conditionGrid.BackColor = System.Drawing.SystemColors.Window;
            this._conditionGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._conditionGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._conditionGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._conditionGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._conditionGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._conditionGrid.Location = new System.Drawing.Point(0, 25);
            this._conditionGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._conditionGrid.Name = "_conditionGrid";
            this._conditionGrid.Size = new System.Drawing.Size(1090, 292);
            this._conditionGrid.TabIndex = 3;
            this._conditionGrid.TabStop = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._loadItemCondition,
            this.toolStripLabel1});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1090, 25);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(63, 22);
            this.toolStripLabel1.Text = "เงื่อนไขพิเศษ";
            // 
            // _loadItemCondition
            // 
            this._loadItemCondition.Image = global::SMLERPIC.Properties.Resources.lightbulb_on;
            this._loadItemCondition.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._loadItemCondition.Name = "_loadItemCondition";
            this._loadItemCondition.Padding = new System.Windows.Forms.Padding(1);
            this._loadItemCondition.ResourceName = "";
            this._loadItemCondition.Size = new System.Drawing.Size(168, 22);
            this._loadItemCondition.Text = "เรียกรายการหน่วยนับ Barcode";
            // 
            // _icSearchLevelScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._panel);
            this.Controls.Add(this._toolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icSearchLevelScreen";
            this.Size = new System.Drawing.Size(1090, 639);
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this._panel.ResumeLayout(false);
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.Panel1.PerformLayout();
            this._splitContainer.Panel2.ResumeLayout(false);
            this._splitContainer.Panel2.PerformLayout();
            this._splitContainer.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStrip _toolStrip;
        public MyLib._myGrid _grid;
        public System.Windows.Forms.ToolStripButton _saveButton;
        public System.Windows.Forms.ToolStripButton _closeButton;
        public MyLib._myPanel _panel;
        public MyLib.ToolStripMyButton _loadItem;
        public System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer _splitContainer;
        public MyLib._myGrid _conditionGrid;
        public System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        public MyLib.ToolStripMyButton _loadItemCondition;
    }
}
