namespace SMLActivesync
{
    partial class _tableSyncControl
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._tableGrid = new MyLib._myGrid();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this._fieldGrid = new MyLib._myGrid();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this._receiveModeButton = new System.Windows.Forms.ToolStripButton();
            this._sendModeButton = new System.Windows.Forms.ToolStripButton();
            this._exchangeModeButton = new System.Windows.Forms.ToolStripButton();
            this._selectAllButton = new System.Windows.Forms.ToolStripButton();
            this._deSelectAllButton = new System.Windows.Forms.ToolStripButton();
            this._saveButton = new System.Windows.Forms.ToolStripButton();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1036, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._tableGrid);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._fieldGrid);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer1.Size = new System.Drawing.Size(1036, 561);
            this.splitContainer1.SplitterDistance = 526;
            this.splitContainer1.TabIndex = 1;
            // 
            // _tableGrid
            // 
            this._tableGrid._extraWordShow = true;
            this._tableGrid._selectRow = -1;
            this._tableGrid.BackColor = System.Drawing.SystemColors.Window;
            this._tableGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._tableGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._tableGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._tableGrid.Location = new System.Drawing.Point(0, 0);
            this._tableGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._tableGrid.Name = "_tableGrid";
            this._tableGrid.Size = new System.Drawing.Size(524, 534);
            this._tableGrid.TabIndex = 0;
            this._tableGrid.TabStop = false;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._receiveModeButton,
            this._sendModeButton,
            this._exchangeModeButton});
            this.toolStrip3.Location = new System.Drawing.Point(0, 534);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(524, 25);
            this.toolStrip3.TabIndex = 1;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // _fieldGrid
            // 
            this._fieldGrid._extraWordShow = true;
            this._fieldGrid._selectRow = -1;
            this._fieldGrid.BackColor = System.Drawing.SystemColors.Window;
            this._fieldGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._fieldGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._fieldGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fieldGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._fieldGrid.Location = new System.Drawing.Point(0, 0);
            this._fieldGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._fieldGrid.Name = "_fieldGrid";
            this._fieldGrid.Size = new System.Drawing.Size(504, 534);
            this._fieldGrid.TabIndex = 1;
            this._fieldGrid.TabStop = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._selectAllButton,
            this._deSelectAllButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 534);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(504, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // _receiveModeButton
            // 
            this._receiveModeButton.Image = global::SMLActivesync.Properties.Resources.inbox_into;
            this._receiveModeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._receiveModeButton.Name = "_receiveModeButton";
            this._receiveModeButton.Size = new System.Drawing.Size(101, 22);
            this._receiveModeButton.Text = "Receive Mode";
            // 
            // _sendModeButton
            // 
            this._sendModeButton.Image = global::SMLActivesync.Properties.Resources.outbox_out;
            this._sendModeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._sendModeButton.Name = "_sendModeButton";
            this._sendModeButton.Size = new System.Drawing.Size(87, 22);
            this._sendModeButton.Text = "Send Mode";
            // 
            // _exchangeModeButton
            // 
            this._exchangeModeButton.Image = global::SMLActivesync.Properties.Resources.outbox;
            this._exchangeModeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._exchangeModeButton.Name = "_exchangeModeButton";
            this._exchangeModeButton.Size = new System.Drawing.Size(111, 22);
            this._exchangeModeButton.Text = "Exchange Mode";
            // 
            // _selectAllButton
            // 
            this._selectAllButton.Image = global::SMLActivesync.Properties.Resources.check2;
            this._selectAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectAllButton.Name = "_selectAllButton";
            this._selectAllButton.Size = new System.Drawing.Size(75, 22);
            this._selectAllButton.Text = "Select All";
            // 
            // _deSelectAllButton
            // 
            this._deSelectAllButton.Image = global::SMLActivesync.Properties.Resources.delete2;
            this._deSelectAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._deSelectAllButton.Name = "_deSelectAllButton";
            this._deSelectAllButton.Size = new System.Drawing.Size(88, 22);
            this._deSelectAllButton.Text = "Deselect All";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLActivesync.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(51, 22);
            this._saveButton.Text = "Save";
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLActivesync.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(56, 22);
            this._closeButton.Text = "Close";
            // 
            // _tableSyncControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "_tableSyncControl";
            this.Size = new System.Drawing.Size(1036, 586);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _saveButton;
        private System.Windows.Forms.ToolStripButton _closeButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MyLib._myGrid _tableGrid;
        private MyLib._myGrid _fieldGrid;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton _selectAllButton;
        private System.Windows.Forms.ToolStripButton _deSelectAllButton;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton _receiveModeButton;
        private System.Windows.Forms.ToolStripButton _sendModeButton;
        private System.Windows.Forms.ToolStripButton _exchangeModeButton;
    }
}
