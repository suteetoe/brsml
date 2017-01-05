namespace SMLERPGL._report
{
    partial class _selectJournalBook
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
            this._bookGrid = new MyLib._myGrid();
            this.SuspendLayout();
            // 
            // _bookGrid
            // 
            this._bookGrid.BackColor = System.Drawing.SystemColors.Window;
            this._bookGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._bookGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._bookGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._bookGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._bookGrid.IsEdit = false;
            this._bookGrid.Location = new System.Drawing.Point(0, 0);
            this._bookGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._bookGrid.Name = "_bookGrid";
            this._bookGrid.Size = new System.Drawing.Size(320, 287);
            this._bookGrid.TabIndex = 1;
            this._bookGrid.TabStop = false;
            // 
            // _selectJournalBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._bookGrid);
            this.Name = "_selectJournalBook";
            this.Size = new System.Drawing.Size(320, 287);
            this.ResumeLayout(false);

        }

        #endregion

        public MyLib._myGrid _bookGrid;

    }
}
