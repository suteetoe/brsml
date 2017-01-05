namespace SMLInventoryControl._icPrice
{
    partial class _icPriceDetail
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
            this._grid = new MyLib._myGrid();
            this._toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._dateAutoButton = new MyLib.ToolStripMyButton();
            this._toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _grid
            // 
            this._grid._extraWordShow = true;
            this._grid._selectRow = -1;
            this._grid.BackColor = System.Drawing.SystemColors.Window;
            this._grid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._grid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._grid.Location = new System.Drawing.Point(0, 25);
            this._grid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._grid.Name = "_grid";
            this._grid.Size = new System.Drawing.Size(895, 420);
            this._grid.TabIndex = 0;
            this._grid.TabStop = false;
            // 
            // _toolStrip1
            // 
            this._toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._dateAutoButton});
            this._toolStrip1.Location = new System.Drawing.Point(0, 0);
            this._toolStrip1.Name = "_toolStrip1";
            this._toolStrip1.Size = new System.Drawing.Size(895, 25);
            this._toolStrip1.TabIndex = 2;
            this._toolStrip1.Text = "toolStrip1";
            // 
            // _dateAutoButton
            // 
            this._dateAutoButton.Image = global::SMLInventoryControl.Properties.Resources.flash;
            this._dateAutoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._dateAutoButton.Name = "_dateAutoButton";
            this._dateAutoButton.Padding = new System.Windows.Forms.Padding(1);
            this._dateAutoButton.ResourceName = "กำหนดวันที่อัตโนมัติ";
            this._dateAutoButton.Size = new System.Drawing.Size(118, 22);
            this._dateAutoButton.Text = "กำหนดวันที่อัตโนมัติ";
            this._dateAutoButton.Click += new System.EventHandler(this._dateAutoButton_Click);
            // 
            // _icPriceDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._grid);
            this.Controls.Add(this._toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icPriceDetail";
            this.Size = new System.Drawing.Size(895, 445);
            this._toolStrip1.ResumeLayout(false);
            this._toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MyLib._myGrid _grid;
        private MyLib.ToolStripMyButton _dateAutoButton;
        public System.Windows.Forms.ToolStrip _toolStrip1;

    }
}
