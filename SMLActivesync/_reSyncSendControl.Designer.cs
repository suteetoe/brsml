﻿namespace SMLActivesync
{
    partial class _reSyncSendControl
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._branchGrid = new MyLib._myGrid();
            this.label1 = new System.Windows.Forms.Label();
            this._tableGrid = new MyLib._myGrid();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this._selectAllButton = new System.Windows.Forms.ToolStripButton();
            this._removeAllButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._processButton = new System.Windows.Forms.ToolStripButton();
            this._branchLabel = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(917, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLActivesync.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(56, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this._branchGrid);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._tableGrid);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer1.Panel2.Controls.Add(this._branchLabel);
            this.splitContainer1.Size = new System.Drawing.Size(917, 590);
            this.splitContainer1.SplitterDistance = 465;
            this.splitContainer1.TabIndex = 3;
            // 
            // _branchGrid
            // 
            this._branchGrid._extraWordShow = true;
            this._branchGrid._selectRow = -1;
            this._branchGrid.BackColor = System.Drawing.SystemColors.Window;
            this._branchGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._branchGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._branchGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._branchGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._branchGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._branchGrid.Location = new System.Drawing.Point(0, 25);
            this._branchGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._branchGrid.Name = "_branchGrid";
            this._branchGrid.Size = new System.Drawing.Size(463, 563);
            this._branchGrid.TabIndex = 0;
            this._branchGrid.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Branch";
            // 
            // _tableGrid
            // 
            this._tableGrid._extraWordShow = true;
            this._tableGrid._selectRow = -1;
            this._tableGrid.BackColor = System.Drawing.SystemColors.Window;
            this._tableGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._tableGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._tableGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._tableGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._tableGrid.Location = new System.Drawing.Point(0, 25);
            this._tableGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._tableGrid.Name = "_tableGrid";
            this._tableGrid.Size = new System.Drawing.Size(446, 538);
            this._tableGrid.TabIndex = 1;
            this._tableGrid.TabStop = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._selectAllButton,
            this._removeAllButton,
            this.toolStripSeparator1,
            this._processButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 563);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(446, 25);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // _selectAllButton
            // 
            this._selectAllButton.Image = global::SMLActivesync.Properties.Resources.check2;
            this._selectAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectAllButton.Name = "_selectAllButton";
            this._selectAllButton.Size = new System.Drawing.Size(75, 22);
            this._selectAllButton.Text = "Select All";
            this._selectAllButton.Click += new System.EventHandler(this._selectAllButton_Click);
            // 
            // _removeAllButton
            // 
            this._removeAllButton.Image = global::SMLActivesync.Properties.Resources.delete2;
            this._removeAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._removeAllButton.Name = "_removeAllButton";
            this._removeAllButton.Size = new System.Drawing.Size(90, 22);
            this._removeAllButton.Text = "Remove All ";
            this._removeAllButton.Click += new System.EventHandler(this._removeAllButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _processButton
            // 
            this._processButton.Image = global::SMLActivesync.Properties.Resources.flash;
            this._processButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(67, 22);
            this._processButton.Text = "Process";
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _branchLabel
            // 
            this._branchLabel.AutoSize = true;
            this._branchLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._branchLabel.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._branchLabel.Location = new System.Drawing.Point(0, 0);
            this._branchLabel.Name = "_branchLabel";
            this._branchLabel.Size = new System.Drawing.Size(0, 25);
            this._branchLabel.TabIndex = 2;
            // 
            // _reSyncSendControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_reSyncSendControl";
            this.Size = new System.Drawing.Size(917, 615);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib._myGrid _tableGrid;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MyLib._myGrid _branchGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label _branchLabel;
        private System.Windows.Forms.ToolStripButton _closeButton;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton _processButton;
        private System.Windows.Forms.ToolStripButton _selectAllButton;
        private System.Windows.Forms.ToolStripButton _removeAllButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
