namespace SMLERPIC._icPromotion
{
    partial class _formula
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
            this._myManageMain = new MyLib._myManageData();
            this.SuspendLayout();
            // 
            // _myManageMain
            // 
            this._myManageMain._mainMenuCode = "";
            this._myManageMain._mainMenuId = "";
            this._myManageMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageMain.Location = new System.Drawing.Point(0, 0);
            this._myManageMain.Name = "_myManageMain";
            this._myManageMain.Size = new System.Drawing.Size(759, 569);
            this._myManageMain.TabIndex = 0;
            this._myManageMain.TabStop = false;
            // 
            // _icPromotionFormula
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageMain);
            this.Name = "_icPromotionFormula";
            this.Size = new System.Drawing.Size(759, 569);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myManageData _myManageMain;
    }
}
