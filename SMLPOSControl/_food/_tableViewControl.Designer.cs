namespace SMLPOSControl._food
{
    partial class _tableViewControl
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._tableLevelPanel = new System.Windows.Forms.Panel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this._orderDocGrid = new MyLib._myGrid();
            this._orderItemGrid = new MyLib._myGrid();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._tableLevelPanel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(1118, 693);
            this.splitContainer2.SplitterDistance = 424;
            this.splitContainer2.TabIndex = 1;
            // 
            // _tableLevelPanel
            // 
            this._tableLevelPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableLevelPanel.Location = new System.Drawing.Point(0, 0);
            this._tableLevelPanel.Name = "_tableLevelPanel";
            this._tableLevelPanel.Size = new System.Drawing.Size(1116, 422);
            this._tableLevelPanel.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this._orderDocGrid);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this._orderItemGrid);
            this.splitContainer3.Size = new System.Drawing.Size(1118, 265);
            this.splitContainer3.SplitterDistance = 359;
            this.splitContainer3.TabIndex = 1;
            // 
            // _orderDocGrid
            // 
            this._orderDocGrid._extraWordShow = true;
            this._orderDocGrid._selectRow = -1;
            this._orderDocGrid.BackColor = System.Drawing.SystemColors.Window;
            this._orderDocGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._orderDocGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._orderDocGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._orderDocGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._orderDocGrid.Location = new System.Drawing.Point(0, 0);
            this._orderDocGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._orderDocGrid.Name = "_orderDocGrid";
            this._orderDocGrid.Size = new System.Drawing.Size(357, 263);
            this._orderDocGrid.TabIndex = 1;
            this._orderDocGrid.TabStop = false;
            // 
            // _orderItemGrid
            // 
            this._orderItemGrid._extraWordShow = true;
            this._orderItemGrid._selectRow = -1;
            this._orderItemGrid.BackColor = System.Drawing.SystemColors.Window;
            this._orderItemGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._orderItemGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._orderItemGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._orderItemGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._orderItemGrid.Location = new System.Drawing.Point(0, 0);
            this._orderItemGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._orderItemGrid.Name = "_orderItemGrid";
            this._orderItemGrid.Size = new System.Drawing.Size(753, 263);
            this._orderItemGrid.TabIndex = 0;
            this._orderItemGrid.TabStop = false;
            // 
            // _tableViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_tableViewControl";
            this.Size = new System.Drawing.Size(1118, 693);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel _tableLevelPanel;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private MyLib._myGrid _orderDocGrid;
        private MyLib._myGrid _orderItemGrid;
    }
}
