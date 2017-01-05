namespace SMLERPControl
{
    partial class _importFromImageForm
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
            this._processStripButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._resultGrid = new MyLib._myGrid();
            this._pictureBox = new System.Windows.Forms.PictureBox();
            this._refreshStripButton = new MyLib.ToolStripMyButton();
            this._stopProcessStripButton = new MyLib.ToolStripMyButton();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::SMLERPControl.Properties.Resources.bt03;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._processStripButton,
            this._refreshStripButton,
            this._stopProcessStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1170, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _processStripButton
            // 
            this._processStripButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._processStripButton.Image = global::SMLERPControl.Properties.Resources.flash;
            this._processStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._processStripButton.Name = "_processStripButton";
            this._processStripButton.Size = new System.Drawing.Size(67, 22);
            this._processStripButton.Text = "Process";
            this._processStripButton.ToolTipText = "Process";
            this._processStripButton.Click += new System.EventHandler(this._processStripButton_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this._resultGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._pictureBox);
            this.splitContainer1.Size = new System.Drawing.Size(1170, 754);
            this.splitContainer1.SplitterDistance = 587;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // _resultGrid
            // 
            this._resultGrid._extraWordShow = true;
            this._resultGrid._selectRow = -1;
            this._resultGrid.BackColor = System.Drawing.SystemColors.Window;
            this._resultGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._resultGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._resultGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._resultGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultGrid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._resultGrid.Location = new System.Drawing.Point(0, 0);
            this._resultGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._resultGrid.Name = "_resultGrid";
            this._resultGrid.Size = new System.Drawing.Size(585, 752);
            this._resultGrid.TabIndex = 0;
            this._resultGrid.TabStop = false;
            // 
            // _pictureBox
            // 
            this._pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pictureBox.Location = new System.Drawing.Point(0, 0);
            this._pictureBox.Name = "_pictureBox";
            this._pictureBox.Size = new System.Drawing.Size(576, 752);
            this._pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._pictureBox.TabIndex = 0;
            this._pictureBox.TabStop = false;
            // 
            // _refreshStripButton
            // 
            this._refreshStripButton.Image = global::SMLERPControl.Properties.Resources.refresh;
            this._refreshStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._refreshStripButton.Name = "_refreshStripButton";
            this._refreshStripButton.Padding = new System.Windows.Forms.Padding(1);
            this._refreshStripButton.ResourceName = "Refresh";
            this._refreshStripButton.Size = new System.Drawing.Size(68, 22);
            this._refreshStripButton.Text = "Refresh";
            this._refreshStripButton.Click += new System.EventHandler(this._refreshStripButton_Click);
            // 
            // _stopProcessStripButton
            // 
            this._stopProcessStripButton.Image = global::SMLERPControl.Properties.Resources.delete2;
            this._stopProcessStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._stopProcessStripButton.Name = "_stopProcessStripButton";
            this._stopProcessStripButton.Padding = new System.Windows.Forms.Padding(1);
            this._stopProcessStripButton.ResourceName = "Stop Procress";
            this._stopProcessStripButton.Size = new System.Drawing.Size(100, 22);
            this._stopProcessStripButton.Text = "Stop Procress";
            this._stopProcessStripButton.Click += new System.EventHandler(this._stopProcessStripButton_Click);
            // 
            // _importFromImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 779);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_importFromImageForm";
            this.Text = "Impot";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _processStripButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MyLib._myGrid _resultGrid;
        private System.Windows.Forms.PictureBox _pictureBox;
        private MyLib.ToolStripMyButton _refreshStripButton;
        private MyLib.ToolStripMyButton _stopProcessStripButton;
    }
}