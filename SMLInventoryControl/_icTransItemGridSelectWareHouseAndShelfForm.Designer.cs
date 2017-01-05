namespace SMLInventoryControl
{
    partial class _icTransItemGridSelectWareHouseAndShelfForm
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
            this._wareHouseAndLocation = new SMLInventoryControl._icmainGridWarehouseLocationControl();
            this.SuspendLayout();
            // 
            // _wareHouseAndShelf
            // 
            this._wareHouseAndLocation._extraWordShow = true;
            this._wareHouseAndLocation.BackColor = System.Drawing.SystemColors.Window;
            this._wareHouseAndLocation.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._wareHouseAndLocation.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._wareHouseAndLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this._wareHouseAndLocation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._wareHouseAndLocation.IsEdit = false;
            this._wareHouseAndLocation.Location = new System.Drawing.Point(0, 0);
            this._wareHouseAndLocation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._wareHouseAndLocation.Name = "_wareHouseAndShelf";
            this._wareHouseAndLocation.Size = new System.Drawing.Size(725, 357);
            this._wareHouseAndLocation.TabIndex = 0;
            this._wareHouseAndLocation.TabStop = false;
            // 
            // _icTransItemGridSelectWareHouseAndShelfForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 357);
            this.Controls.Add(this._wareHouseAndLocation);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "_icTransItemGridSelectWareHouseAndShelfForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_icTransItemGridSelectWareHouseAndShelfForm";
            this.ResumeLayout(false);

        }

        #endregion

        private _icmainGridWarehouseLocationControl _wareHouseAndLocation;
    }
}