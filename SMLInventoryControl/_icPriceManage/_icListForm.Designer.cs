namespace SMLInventoryControl._icPriceManage
{
    partial class _icListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_icListForm));
            this._icGrid = new MyLib._myGrid();
            this.SuspendLayout();
            // 
            // _icGrid
            // 
            this._icGrid._extraWordShow = true;
            this._icGrid._selectRow = -1;
            this._icGrid.BackColor = System.Drawing.SystemColors.Window;
            this._icGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._icGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._icGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icGrid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._icGrid.Location = new System.Drawing.Point(0, 0);
            this._icGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._icGrid.Name = "_icGrid";
            this._icGrid.Size = new System.Drawing.Size(784, 562);
            this._icGrid.TabIndex = 0;
            this._icGrid.TabStop = false;
            // 
            // _icListForm
            // 
            this._colorBackground = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this._icGrid);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "_icListForm";
            this.ResourceName = "ข้อมูลสินค้า";
            this.Text = "ข้อมูลสินค้า";
            this.ResumeLayout(false);

        }

        #endregion

        public MyLib._myGrid _icGrid;

    }
}