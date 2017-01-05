namespace SMLERPAPARControl
{
    partial class _advanceControl
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
            this._dataGrid = new SMLERPAPARControl._payDepositAdvanceGridControl();
            this.SuspendLayout();
            // 
            // _dataGrid
            // 
            this._dataGrid._extraWordShow = true;
            this._dataGrid._selectRow = -1;
            this._dataGrid.AllowDrop = true;
            this._dataGrid.AutoSize = true;
            this._dataGrid.BackColor = System.Drawing.SystemColors.Window;
            this._dataGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._dataGrid.ColumnBackgroundAuto = false;
            this._dataGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._dataGrid.Location = new System.Drawing.Point(0, 0);
            this._dataGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._dataGrid.Name = "_dataGrid";
            this._dataGrid.RowOddBackground = System.Drawing.Color.AliceBlue;
            this._dataGrid.ShowTotal = true;
            this._dataGrid.Size = new System.Drawing.Size(902, 619);
            this._dataGrid.TabIndex = 1;
            this._dataGrid.TabStop = false;
            // 
            // _advanceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._dataGrid);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_advanceControl";
            this.Size = new System.Drawing.Size(902, 619);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public _payDepositAdvanceGridControl _dataGrid;
    }
}
