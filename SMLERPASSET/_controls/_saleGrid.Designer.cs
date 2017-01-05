namespace SMLERPASSET._controls
{
    partial class _saleGrid
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
            this.components = new System.ComponentModel.Container();
            this._myGrid1 = new MyLib._myGrid();
            this.SuspendLayout();
            // 
            // _myGrid1
            // 
            this._myGrid1.BackColor = System.Drawing.SystemColors.Window;
            this._myGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myGrid1.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            this._myGrid1.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._myGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myGrid1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myGrid1.Location = new System.Drawing.Point(0, 0);
            this._myGrid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._myGrid1.Name = "_myGrid1";
            this._myGrid1.Size = new System.Drawing.Size(414, 428);
            this._myGrid1.TabIndex = 0;
            this._myGrid1.TabStop = false;
            // 
            // _saleGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myGrid1);
            this.Name = "_saleGrid";
            this.Size = new System.Drawing.Size(414, 428);
            this.ResumeLayout(false);

        }

        #endregion

        public MyLib._myGrid _myGrid1;

    }
}
