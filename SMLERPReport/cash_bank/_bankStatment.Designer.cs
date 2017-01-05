namespace SMLERPReport.cash_bank
{
    partial class _bankStatment
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
            this._dock = new Crom.Controls.Docking.DockContainer();
            this.SuspendLayout();
            // 
            // _dock
            // 
            this._dock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(118)))), ((int)(((byte)(118)))));
            this._dock.CanMoveByMouseFilledForms = true;
            this._dock.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dock.Location = new System.Drawing.Point(0, 0);
            this._dock.Name = "_dock";
            this._dock.Size = new System.Drawing.Size(989, 684);
            this._dock.TabIndex = 0;
            this._dock.TitleBarGradientColor1 = System.Drawing.SystemColors.Control;
            this._dock.TitleBarGradientColor2 = System.Drawing.Color.White;
            this._dock.TitleBarGradientSelectedColor1 = System.Drawing.Color.DarkGray;
            this._dock.TitleBarGradientSelectedColor2 = System.Drawing.Color.White;
            this._dock.TitleBarTextColor = System.Drawing.Color.Black;
            // 
            // _bankStatment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._dock);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_bankStatment";
            this.Size = new System.Drawing.Size(989, 684);
            this.ResumeLayout(false);

        }

        #endregion

        private Crom.Controls.Docking.DockContainer _dock;
    }
}
