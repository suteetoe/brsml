namespace SMLERPIC
{
    partial class _icBarcodeDiscountControl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this._icBarcodeDiscountScreen = new SMLERPIC._icBarcodeDiscountScreenControl();
            this._tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this._icBarcodeDiscountScreen);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(720, 79);
            this.panel1.TabIndex = 2;
            // 
            // _icBarcodeDiscountScreen
            // 
            this._icBarcodeDiscountScreen._isChange = false;
            this._icBarcodeDiscountScreen.AutoSize = true;
            this._icBarcodeDiscountScreen.BackColor = System.Drawing.Color.Transparent;
            this._icBarcodeDiscountScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icBarcodeDiscountScreen.Enabled = false;
            this._icBarcodeDiscountScreen.Location = new System.Drawing.Point(5, 5);
            this._icBarcodeDiscountScreen.Name = "_icBarcodeDiscountScreen";
            this._icBarcodeDiscountScreen.Size = new System.Drawing.Size(710, 69);
            this._icBarcodeDiscountScreen.TabIndex = 0;
            // 
            // _tableLayoutPanel
            // 
            this._tableLayoutPanel.ColumnCount = 2;
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableLayoutPanel.Location = new System.Drawing.Point(0, 79);
            this._tableLayoutPanel.Name = "_tableLayoutPanel";
            this._tableLayoutPanel.RowCount = 3;
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this._tableLayoutPanel.Size = new System.Drawing.Size(720, 368);
            this._tableLayoutPanel.TabIndex = 3;
            // 
            // _icBarcodeDiscountControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._tableLayoutPanel);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icBarcodeDiscountControl";
            this.Size = new System.Drawing.Size(720, 447);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TableLayoutPanel _tableLayoutPanel;
        public _icBarcodeDiscountScreenControl _icBarcodeDiscountScreen;
    }
}
