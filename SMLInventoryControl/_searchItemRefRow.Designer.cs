namespace SMLInventoryControl
{
    partial class _searchItemRefRow
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
            this._gridDetail = new MyLib._myGrid();
            this.SuspendLayout();
            // 
            // _gridDetail
            // 
            this._gridDetail._extraWordShow = true;
            this._gridDetail._selectRow = -1;
            this._gridDetail.BackColor = System.Drawing.SystemColors.Window;
            this._gridDetail.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridDetail.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridDetail.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridDetail.Location = new System.Drawing.Point(0, 0);
            this._gridDetail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridDetail.Name = "_gridDetail";
            this._gridDetail.Size = new System.Drawing.Size(722, 330);
            this._gridDetail.TabIndex = 0;
            this._gridDetail.TabStop = false;
            // 
            // _searchItemRefRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 330);
            this.Controls.Add(this._gridDetail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_searchItemRefRow";
            this.Text = "ค้นหารายการอ้างอิงสินค้า";
            this.ResumeLayout(false);

        }

        #endregion

        public MyLib._myGrid _gridDetail;
    }
}