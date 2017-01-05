namespace SMLInventoryControl
{
    partial class _icTransWeightControl
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
            this._grid = new MyLib._myGrid();
            this._screen = new MyLib._myScreen();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(676, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _grid
            // 
            this._grid._extraWordShow = true;
            this._grid._selectRow = -1;
            this._grid.BackColor = System.Drawing.SystemColors.Window;
            this._grid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._grid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._grid.Location = new System.Drawing.Point(0, 37);
            this._grid.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this._grid.Name = "_grid";
            this._grid.Size = new System.Drawing.Size(676, 484);
            this._grid.TabIndex = 2;
            this._grid.TabStop = false;
            // 
            // _screen
            // 
            this._screen._isChange = false;
            this._screen.BackColor = System.Drawing.Color.Transparent;
            this._screen.Dock = System.Windows.Forms.DockStyle.Top;
            this._screen.Location = new System.Drawing.Point(0, 25);
            this._screen.Name = "_screen";
            this._screen.Size = new System.Drawing.Size(676, 12);
            this._screen.TabIndex = 1;
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLInventoryControl.Properties.Resources.flash;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "Save";
            this._saveButton.Size = new System.Drawing.Size(53, 22);
            this._saveButton.Text = "Save";
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLInventoryControl.Properties.Resources.error1;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "Close";
            this._closeButton.Size = new System.Drawing.Size(58, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _icTransWeightControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 521);
            this.Controls.Add(this._grid);
            this.Controls.Add(this._screen);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_icTransWeightControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        public MyLib.ToolStripMyButton _saveButton;
        public MyLib.ToolStripMyButton _closeButton;
        public MyLib._myScreen _screen;
        public MyLib._myGrid _grid;
    }
}
