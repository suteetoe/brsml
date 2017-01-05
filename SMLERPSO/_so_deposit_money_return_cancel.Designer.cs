namespace SMLERPSO
{
    partial class _so_deposit_money_return_cancel
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
            this._icTrans = new SMLInventoryControl._icTransControl();
            this.SuspendLayout();
            // 
            // _icTrans
            // 
            this._icTrans.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icTrans.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._icTrans._transControlType = _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก;
            this._icTrans.Location = new System.Drawing.Point(0, 0);
            this._icTrans.Name = "_icTrans";
            this._icTrans.Size = new System.Drawing.Size(724, 580);
            this._icTrans.TabIndex = 0;
            // 
            // _so_advance_money_return_cancel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._icTrans);
            this.Name = "_so_advance_money_return_cancel";
            this.Size = new System.Drawing.Size(724, 580);
            this.ResumeLayout(false);

        }

        #endregion

        private SMLInventoryControl._icTransControl _icTrans;
    }
}
