namespace SMLERPASSET
{
    partial class _as_transfer
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
            this._myManageData1 = new MyLib._myManageData();
            this._detailScreen = new SMLERPASSET._as_transferScreenControl();
            this.SuspendLayout();
            // 
            // _myManageData1
            // 
            this._myManageData1._mainMenuCode = "";
            this._myManageData1._mainMenuId = "";
            this._myManageData1.BackColor = System.Drawing.Color.LightCyan;
            this._myManageData1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageData1.Location = new System.Drawing.Point(0, 0);
            this._myManageData1.Name = "_myManageData1";
            this._myManageData1.Size = new System.Drawing.Size(531, 485);
            this._myManageData1.TabIndex = 0;
            this._myManageData1.TabStop = false;

            this._myManageData1._form2.Controls.Add(this._detailScreen);
            // 
            // _detailScreen
            // 
            this._detailScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._detailScreen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._detailScreen.Location = new System.Drawing.Point(0, 0);
            this._detailScreen.Name = "_detailScreen";
            this._detailScreen.Size = new System.Drawing.Size(531, 485);
            this._detailScreen.TabIndex = 1;
            // 
            // _as_transfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData1);
            this.Name = "_as_transfer";
            this.Size = new System.Drawing.Size(531, 485);
            this.ResumeLayout(false);

        }

        #endregion
        private MyLib._myManageData _myManageData1;
        private _as_transferScreenControl _detailScreen;
        //private System.Windows.Forms.TabPage tabPage2;
    }
}
