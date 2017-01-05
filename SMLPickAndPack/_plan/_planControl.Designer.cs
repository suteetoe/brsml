namespace SMLPickAndPack._plan
{
    partial class _planControl
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
            this._manageData = new MyLib._myManageData();
            this.SuspendLayout();
            // 
            // _manageData
            // 
            this._manageData._mainMenuCode = "";
            this._manageData._mainMenuId = "";
            this._manageData.BackColor = System.Drawing.Color.WhiteSmoke;
            this._manageData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._manageData.Dock = System.Windows.Forms.DockStyle.Fill;
            this._manageData.Location = new System.Drawing.Point(0, 0);
            this._manageData.Name = "_manageData";
            this._manageData.Size = new System.Drawing.Size(930, 657);
            this._manageData.TabIndex = 0;
            this._manageData.TabStop = false;
            // 
            // _planControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._manageData);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_planControl";
            this.Size = new System.Drawing.Size(930, 657);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myManageData _manageData;
    }
}
