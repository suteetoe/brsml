namespace SMLERPControl._coupon
{
    partial class _couponListControl
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
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._saveButton = new System.Windows.Forms.ToolStripButton();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this._myPanel = new MyLib._myPanel();
            this._movementGrid = new MyLib._myGrid();
            this._screen = new MyLib._myScreen();
            this._toolStrip.SuspendLayout();
            this._myPanel.SuspendLayout();
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
            this._toolStrip.Size = new System.Drawing.Size(904, 25);
            this._toolStrip.TabIndex = 2;
            this._toolStrip.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPControl.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(80, 22);
            this._saveButton.Text = "Save (F12)";
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPControl.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(56, 22);
            this._closeButton.Text = "Close";
            // 
            // _myPanel
            // 
            this._myPanel._switchTabAuto = false;
            this._myPanel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel.Controls.Add(this._movementGrid);
            this._myPanel.Controls.Add(this._screen);
            this._myPanel.CornerPicture = null;
            this._myPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel.Location = new System.Drawing.Point(0, 25);
            this._myPanel.Name = "_myPanel";
            this._myPanel.Size = new System.Drawing.Size(904, 555);
            this._myPanel.TabIndex = 3;
            // 
            // _movementGrid
            // 
            this._movementGrid._extraWordShow = true;
            this._movementGrid._selectRow = -1;
            this._movementGrid.BackColor = System.Drawing.SystemColors.Window;
            this._movementGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._movementGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._movementGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._movementGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._movementGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._movementGrid.Location = new System.Drawing.Point(0, 10);
            this._movementGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._movementGrid.Name = "_movementGrid";
            this._movementGrid.Size = new System.Drawing.Size(904, 545);
            this._movementGrid.TabIndex = 1;
            this._movementGrid.TabStop = false;
            // 
            // _screen
            // 
            this._screen._isChange = false;
            this._screen.BackColor = System.Drawing.Color.Transparent;
            this._screen.Dock = System.Windows.Forms.DockStyle.Top;
            this._screen.Location = new System.Drawing.Point(0, 0);
            this._screen.Name = "_screen";
            this._screen.Size = new System.Drawing.Size(904, 10);
            this._screen.TabIndex = 0;
            // 
            // _couponListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel);
            this.Controls.Add(this._toolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_couponListControl";
            this.Size = new System.Drawing.Size(904, 580);
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this._myPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStripButton _saveButton;
        public System.Windows.Forms.ToolStrip _toolStrip;
        public MyLib._myPanel _myPanel;
        public System.Windows.Forms.ToolStripButton _closeButton;
        public MyLib._myScreen _screen;
        public MyLib._myGrid _movementGrid;
    }
}
