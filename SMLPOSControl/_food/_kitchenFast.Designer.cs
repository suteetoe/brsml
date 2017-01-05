namespace SMLPOSControl._food
{
    partial class _kitchenFast
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
            this._buttonClose = new MyLib.ToolStripMyButton();
            this._grid = new MyLib._myGrid();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::SMLPOSControl.Properties.Resources.bt03;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1085, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _buttonClose
            // 
            this._buttonClose.Image = global::SMLPOSControl.Properties.Resources.error1;
            this._buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Padding = new System.Windows.Forms.Padding(1);
            this._buttonClose.ResourceName = "";
            this._buttonClose.Size = new System.Drawing.Size(58, 22);
            this._buttonClose.Text = "Close";
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
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
            this._grid.Size = new System.Drawing.Size(1085, 748);
            this._grid.TabIndex = 1;
            this._grid.TabStop = false;
            // 
            // _kitchenFast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._grid);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_kitchenFast";
            this.Size = new System.Drawing.Size(1085, 773);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton _buttonClose;
        private MyLib._myGrid _grid;
    }
}
