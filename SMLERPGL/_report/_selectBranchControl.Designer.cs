namespace SMLERPGL._report
{
    partial class _selectBranchControl
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
            this._gridBranch = new MyLib._myGrid();
            this.SuspendLayout();
            // 
            // _gridBranch
            // 
            this._gridBranch._extraWordShow = true;
            this._gridBranch._selectRow = -1;
            this._gridBranch.BackColor = System.Drawing.SystemColors.Window;
            this._gridBranch.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridBranch.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridBranch.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridBranch.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridBranch.Location = new System.Drawing.Point(0, 0);
            this._gridBranch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridBranch.Name = "_gridBranch";
            this._gridBranch.Size = new System.Drawing.Size(412, 410);
            this._gridBranch.TabIndex = 1;
            this._gridBranch.TabStop = false;
            // 
            // _selectBranchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._gridBranch);
            this.Name = "_selectBranchControl";
            this.Size = new System.Drawing.Size(412, 410);
            this.ResumeLayout(false);

        }

        #endregion

        public MyLib._myGrid _gridBranch;
    }
}
