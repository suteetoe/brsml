namespace SMLPPControl
{
    partial class _shipmentControl
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
            this._myManageData = new MyLib._myManageData();
            this._shipmentDetailControl1 = new SMLPPControl._shipmentDetailControl();
            this.SuspendLayout();
            // 
            // _myManageData
            // 
            this._myManageData._mainMenuCode = "";
            this._myManageData._mainMenuId = "";
            this._myManageData.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageData.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageData.Location = new System.Drawing.Point(0, 0);
            this._myManageData.Name = "_myManageData";
            this._myManageData.Size = new System.Drawing.Size(745, 843);
            this._myManageData.TabIndex = 0;
            this._myManageData.TabStop = false;
            // 
            // _shipmentDetailControl1
            // 
            this._shipmentDetailControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._shipmentDetailControl1.Location = new System.Drawing.Point(0, 0);
            this._shipmentDetailControl1.Name = "_shipmentDetailControl1";
            this._shipmentDetailControl1.Size = new System.Drawing.Size(745, 843);
            this._shipmentDetailControl1.TabIndex = 1;
            this._shipmentDetailControl1.transControlType = SMLPPGlobal.g._ppControlTypeEnum.ว่าง;
            // 
            // _shipmentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._shipmentDetailControl1);
            this.Controls.Add(this._myManageData);
            this.Name = "_shipmentControl";
            this.Size = new System.Drawing.Size(745, 843);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myManageData _myManageData;
        private _shipmentDetailControl _shipmentDetailControl1;

    }
}
