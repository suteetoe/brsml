namespace SMLInventoryControl._icPrice
{
    partial class _icDiscount
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
            this._myManageDetail = new MyLib._myManageData();
            this._icDiscountControl1 = new SMLInventoryControl._icPrice._icDiscountControl();
            this.SuspendLayout();
            // 
            // _myManageDetail
            // 
            this._myManageDetail._mainMenuCode = "";
            this._myManageDetail._mainMenuId = "";
            this._myManageDetail.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageDetail.Location = new System.Drawing.Point(0, 0);
            this._myManageDetail.Name = "_myManageDetail";
            this._myManageDetail.Size = new System.Drawing.Size(951, 922);
            this._myManageDetail.TabIndex = 2;
            this._myManageDetail.TabStop = false;

            this._myManageDetail._form2.Controls.Add(this._icDiscountControl1);
            // 
            // _icDiscountControl1
            // 
            this._icDiscountControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icDiscountControl1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._icDiscountControl1.Location = new System.Drawing.Point(0, 0);
            this._icDiscountControl1.Name = "_icDiscountControl1";
            this._icDiscountControl1.Size = new System.Drawing.Size(951, 922);
            this._icDiscountControl1.TabIndex = 3;
            // 
            // _icDiscount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageDetail);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icDiscount";
            this.Size = new System.Drawing.Size(951, 922);
            this.ResumeLayout(false);

        }

        #endregion
        private MyLib._myManageData _myManageDetail;
        private _icDiscountControl _icDiscountControl1;
    }
}
