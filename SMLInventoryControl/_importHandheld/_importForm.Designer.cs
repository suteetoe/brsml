namespace SMLInventoryControl._importHandheld
{
    partial class _importHandheldForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._sendButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._dataList = new MyLib._myDataList();
            this._itemGrid = new MyLib._myGrid();
            this._appendButton = new MyLib.ToolStripMyButton();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._sendButton,
            this._appendButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(828, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _sendButton
            // 
            this._sendButton.Image = global::SMLInventoryControl.Properties.Resources.add;
            this._sendButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._sendButton.Name = "_sendButton";
            this._sendButton.Size = new System.Drawing.Size(91, 22);
            this._sendButton.Text = "Import Now";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._dataList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._itemGrid);
            this.splitContainer1.Size = new System.Drawing.Size(828, 617);
            this.splitContainer1.SplitterDistance = 308;
            this.splitContainer1.TabIndex = 3;
            // 
            // _dataList
            // 
            this._dataList._extraWhere = "";
            this._dataList._multiSelect = false;
            this._dataList._multiSelectColumnName = "";
            this._dataList.BackColor = System.Drawing.Color.WhiteSmoke;
            this._dataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._dataList.Location = new System.Drawing.Point(0, 0);
            this._dataList.Margin = new System.Windows.Forms.Padding(0);
            this._dataList.Name = "_dataList";
            this._dataList.Size = new System.Drawing.Size(826, 306);
            this._dataList.TabIndex = 1;
            // 
            // _itemGrid
            // 
            this._itemGrid._extraWordShow = true;
            this._itemGrid._selectRow = -1;
            this._itemGrid.BackColor = System.Drawing.SystemColors.Window;
            this._itemGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._itemGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._itemGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._itemGrid.Location = new System.Drawing.Point(0, 0);
            this._itemGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._itemGrid.Name = "_itemGrid";
            this._itemGrid.Size = new System.Drawing.Size(826, 303);
            this._itemGrid.TabIndex = 2;
            this._itemGrid.TabStop = false;
            // 
            // _appendButton
            // 
            this._appendButton.Image = global::SMLInventoryControl.Properties.Resources.flash;
            this._appendButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._appendButton.Name = "_appendButton";
            this._appendButton.Padding = new System.Windows.Forms.Padding(1);
            this._appendButton.ResourceName = "Import Append Mode";
            this._appendButton.Size = new System.Drawing.Size(144, 22);
            this._appendButton.Text = "Import Append Mode";
            // 
            // _importHandheldForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 642);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_importHandheldForm";
            this.Text = "_importHandheldForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        public MyLib._myDataList _dataList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.ToolStripButton _sendButton;
        public MyLib._myGrid _itemGrid;
        public MyLib.ToolStripMyButton _appendButton;
    }
}