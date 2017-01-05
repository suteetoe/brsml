namespace SMLReport._report
{
    partial class _reportDrawPaper
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
            this._area = new MyLib._myPanel();
            this.SuspendLayout();
            // 
            // _area
            // 
            this._area.BackColor = System.Drawing.Color.White;
            this._area.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._area.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._area.Location = new System.Drawing.Point(113, 72);
            this._area.Name = "_area";
            this._area.ShowBackground = false;
            this._area.Size = new System.Drawing.Size(255, 217);
            this._area.TabIndex = 0;
            // 
            // _reportDrawPaper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this._area);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_reportDrawPaper";
            this.Size = new System.Drawing.Size(636, 594);
            this.ResumeLayout(false);

        }

        #endregion

        public MyLib._myPanel _area;





    }
}
