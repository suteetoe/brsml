namespace SMLInventoryControl._icPriceManage
{
    partial class _icPriceListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_icPriceListForm));
            this._gridIcPriceList1 = new SMLInventoryControl._icPriceManage._gridIcPriceList();
            this.SuspendLayout();
            // 
            // _gridIcPriceList1
            // 
            this._gridIcPriceList1._extraWordShow = true;
            this._gridIcPriceList1._selectRow = -1;
            this._gridIcPriceList1.BackColor = System.Drawing.SystemColors.Window;
            this._gridIcPriceList1.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridIcPriceList1.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridIcPriceList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridIcPriceList1.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridIcPriceList1.IsEdit = false;
            this._gridIcPriceList1.Location = new System.Drawing.Point(0, 0);
            this._gridIcPriceList1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridIcPriceList1.Name = "_gridIcPriceList1";
            this._gridIcPriceList1.Size = new System.Drawing.Size(784, 562);
            this._gridIcPriceList1.TabIndex = 2;
            this._gridIcPriceList1.TabStop = false;
            this._gridIcPriceList1.WidthByPersent = false;
            // 
            // _icPriceListForm
            // 
            this._colorBackground = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this._gridIcPriceList1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "_icPriceListForm";
            this.Text = "ปรับปรุงราคาสินค้า";
            this.ResumeLayout(false);

        }

        #endregion

        private _gridIcPriceList _gridIcPriceList1;
    }
}