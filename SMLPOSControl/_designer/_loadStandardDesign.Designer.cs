﻿namespace SMLPOSControl._designer
{
    partial class _loadStandardDesign
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
            this._dataGrid = new MyLib._myGrid();
            this.SuspendLayout();
            // 
            // _dataGrid
            // 
            this._dataGrid._extraWordShow = true;
            this._dataGrid._selectRow = -1;
            this._dataGrid.BackColor = System.Drawing.SystemColors.Window;
            this._dataGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._dataGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._dataGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._dataGrid.Location = new System.Drawing.Point(0, 0);
            this._dataGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._dataGrid.Name = "_dataGrid";
            this._dataGrid.Size = new System.Drawing.Size(494, 441);
            this._dataGrid.TabIndex = 0;
            this._dataGrid.TabStop = false;
            // 
            // _loadStandardDesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 443);
            this.Controls.Add(this._dataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_loadStandardDesign";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Load POS Design";
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myGrid _dataGrid;
    }
}