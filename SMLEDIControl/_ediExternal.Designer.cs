namespace SMLEDIControl
{
    partial class _ediExternal
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
            this._ediExternalScreenControl = new SMLEDIControl._ediExternalScreenControl();
            this._myManageData1 = new MyLib._myManageData();
            this.SuspendLayout();
            // 
            // _ediExternalScreenControl
            // 
            this._ediExternalScreenControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ediExternalScreenControl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._ediExternalScreenControl.Location = new System.Drawing.Point(0, 0);
            this._ediExternalScreenControl.Name = "_ediExternalScreenControl";
            this._ediExternalScreenControl.Size = new System.Drawing.Size(513, 656);
            this._ediExternalScreenControl.TabIndex = 0;
            // 
            // _myManageData1
            // 
            this._myManageData1._mainMenuCode = "";
            this._myManageData1._mainMenuId = "";
            this._myManageData1.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageData1.Location = new System.Drawing.Point(0, 0);
            this._myManageData1.Name = "_myManageData1";
            this._myManageData1.Size = new System.Drawing.Size(513, 656);
            this._myManageData1.TabIndex = 1;
            this._myManageData1.TabStop = false;
            this._myManageData1._form2.Controls.Add(this._ediExternalScreenControl);

            // 
            // _ediExternal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_ediExternal";
            this.Size = new System.Drawing.Size(513, 656);
            this.ResumeLayout(false);

        }

        #endregion

        private _ediExternalScreenControl _ediExternalScreenControl;
        private MyLib._myManageData _myManageData1;
    }
}
