namespace SMLERPPO
{
    partial class _po_deposit_money
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
            this._po_so_deposit_control1 = new SMLERPAPARControl._depositControl._po_so_deposit_control();
            this.SuspendLayout();
            // 
            // _po_so_deposit_control1
            // 
            this._po_so_deposit_control1._icTransControlType = _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ;
            this._po_so_deposit_control1._screen_code = "";
            this._po_so_deposit_control1.AutoSize = true;
            this._po_so_deposit_control1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._po_so_deposit_control1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._po_so_deposit_control1.Location = new System.Drawing.Point(0, 0);
            this._po_so_deposit_control1.Name = "_po_so_deposit_control1";
            this._po_so_deposit_control1.Size = new System.Drawing.Size(825, 621);
            this._po_so_deposit_control1.TabIndex = 0;
            // 
            // _po_deposit_money
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._po_so_deposit_control1);
            this.Name = "_po_deposit_money";
            this.Size = new System.Drawing.Size(825, 621);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SMLERPAPARControl._depositControl._po_so_deposit_control _po_so_deposit_control1;
    }
}
