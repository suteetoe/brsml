namespace SMLERPGLControl
{
    partial class _pettyCashReceive
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
            this._pettyCashGrid = new MyLib._myGrid();
            this.SuspendLayout();
            // 
            // _pettyCashGrid
            // 
            this._pettyCashGrid.BackColor = System.Drawing.SystemColors.Window;
            this._pettyCashGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pettyCashGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._pettyCashGrid.Location = new System.Drawing.Point(0, 0);
            this._pettyCashGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._pettyCashGrid.Name = "_pettyCashGrid";
            this._pettyCashGrid.Size = new System.Drawing.Size(532, 232);
            this._pettyCashGrid.TabIndex = 0;
            this._pettyCashGrid.TabStop = false;
            // 
            // _pettyCashReceive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._pettyCashGrid);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_pettyCashReceive";
            this.Size = new System.Drawing.Size(532, 232);
            this.ResumeLayout(false);

        }

        #endregion

        public MyLib._myGrid _pettyCashGrid;

    }
}
