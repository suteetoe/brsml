namespace SMLInventoryControl._icPrice
{
    partial class _icAllPrice
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
            this._icAllPriceControl = new SMLInventoryControl._icPrice._icAllPriceControl();
            this.SuspendLayout();
            // 
            // _myManageData1
            // 
            this._myManageDetail._mainMenuCode = "";
            this._myManageDetail._mainMenuId = "";
            this._myManageDetail.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageDetail.Location = new System.Drawing.Point(0, 0);
            this._myManageDetail.Name = "_myManageData1";
            this._myManageDetail.Size = new System.Drawing.Size(835, 745);
            this._myManageDetail.TabIndex = 0;
            this._myManageDetail.TabStop = false;

            this._myManageDetail._form2.Controls.Add(this._icAllPriceControl);
            // 
            // _icAllPriceControl1
            // 
            this._icAllPriceControl.Location = new System.Drawing.Point(167, 47);
            this._icAllPriceControl.Name = "_icAllPriceControl1";
            this._icAllPriceControl.Size = new System.Drawing.Size(562, 698);
            this._icAllPriceControl.TabIndex = 1;
            this._icAllPriceControl.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // _icAllPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.Controls.Add(this._icAllPriceControl1);
            this.Controls.Add(this._myManageDetail);
            this.Name = "_icAllPrice";
            this.Size = new System.Drawing.Size(835, 745);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myManageData _myManageDetail;
        private _icAllPriceControl _icAllPriceControl;
    }
}
