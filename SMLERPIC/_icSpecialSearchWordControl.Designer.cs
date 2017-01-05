namespace SMLERPIC
{
    partial class _icSpecialSearchWordControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_icSpecialSearchWordControl));
            this._gridPiston = new SMLERPIC._gridICSpecialSearchWord();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this._screenICSpecialSearchWord = new SMLERPIC._screenICSpecialSearchWord();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _gridPiston
            // 
            this._gridPiston._extraWordShow = true;
            this._gridPiston._selectRow = -1;
            this._gridPiston.BackColor = System.Drawing.SystemColors.Window;
            this._gridPiston.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridPiston.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridPiston.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridPiston.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridPiston.Location = new System.Drawing.Point(0, 228);
            this._gridPiston.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridPiston.Name = "_gridPiston";
            this._gridPiston.Size = new System.Drawing.Size(965, 575);
            this._gridPiston.TabIndex = 1;
            this._gridPiston.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip1.BackgroundImage")));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 203);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(965, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(28, 22);
            this.toolStripLabel1.Text = "Part";
            // 
            // _screenICSpecialSearchWord
            // 
            this._screenICSpecialSearchWord._isChange = false;
            this._screenICSpecialSearchWord.AutoSize = true;
            this._screenICSpecialSearchWord.BackColor = System.Drawing.Color.Transparent;
            this._screenICSpecialSearchWord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._screenICSpecialSearchWord.Dock = System.Windows.Forms.DockStyle.Top;
            this._screenICSpecialSearchWord.Location = new System.Drawing.Point(0, 0);
            this._screenICSpecialSearchWord.Name = "_screenICSpecialSearchWord";
            this._screenICSpecialSearchWord.Size = new System.Drawing.Size(965, 203);
            this._screenICSpecialSearchWord.TabIndex = 0;
            // 
            // _icSpecialSearchWordControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._gridPiston);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this._screenICSpecialSearchWord);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icSpecialSearchWordControl";
            this.Size = new System.Drawing.Size(965, 803);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        public _screenICSpecialSearchWord _screenICSpecialSearchWord;
        public _gridICSpecialSearchWord _gridPiston;
    }
}
