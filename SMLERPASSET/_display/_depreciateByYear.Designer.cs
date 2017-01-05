namespace SMLERPASSET._display
{
    partial class _depreciateByYear
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
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._processButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._myPanel1 = new MyLib._myPanel();
            this._selectAsset1 = new SMLERPASSET._display._selectAsset();
            this._myGrid1 = new MyLib._myGrid();
            this._myToolBar.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myToolBar
            // 
            this._myToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._processButton,
            this._closeButton});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(372, 25);
            this._myToolBar.TabIndex = 16;
            this._myToolBar.Text = "toolStrip2";
            // 
            // _processButton
            // 
            this._processButton.Image = global::SMLERPASSET.Properties.Resources.flash;
            this._processButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._processButton.Name = "_processButton";
            this._processButton.ResourceName = "ประมวลผล";
            this._processButton.Padding = new System.Windows.Forms.Padding(1);
            this._processButton.Size = new System.Drawing.Size(108, 22);
            this._processButton.Text = "ประมวลผล (F12)";
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPASSET.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.ResourceName = "ปิดหน้าจอ";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.Size = new System.Drawing.Size(75, 22);
            this._closeButton.Text = "ปิดหน้าจอ";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _myPanel1
            // 
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._selectAsset1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel1.Size = new System.Drawing.Size(372, 32);
            this._myPanel1.TabIndex = 17;
            // 
            // _selectAsset1
            // 
            this._selectAsset1.AutoSize = true;
            this._selectAsset1.BackColor = System.Drawing.Color.Transparent;
            this._selectAsset1.Dock = System.Windows.Forms.DockStyle.Top;
            this._selectAsset1.Location = new System.Drawing.Point(5, 5);
            this._selectAsset1.Name = "_selectAsset1";
            this._selectAsset1.ScreenType = SMLERPASSET._display._selectAssetType._depreciateByYear;
            this._selectAsset1.Size = new System.Drawing.Size(362, 23);
            this._selectAsset1.TabIndex = 0;
            // 
            // _myGrid1
            // 
            this._myGrid1.BackColor = System.Drawing.SystemColors.Window;
            this._myGrid1.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._myGrid1.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._myGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myGrid1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myGrid1.Location = new System.Drawing.Point(0, 57);
            this._myGrid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._myGrid1.Name = "_myGrid1";
            this._myGrid1.Size = new System.Drawing.Size(372, 287);
            this._myGrid1.TabIndex = 18;
            this._myGrid1.TabStop = false;
            // 
            // _depreciateByYear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myGrid1);
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this._myToolBar);
            this.Name = "_depreciateByYear";
            this.Size = new System.Drawing.Size(372, 344);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _processButton;
        private MyLib.ToolStripMyButton _closeButton;
        private MyLib._myPanel _myPanel1;
        private _selectAsset _selectAsset1;
        private MyLib._myGrid _myGrid1;
    }
}
