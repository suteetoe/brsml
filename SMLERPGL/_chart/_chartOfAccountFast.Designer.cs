namespace SMLERPGL._chart
{
    partial class _chartOfAccountFast
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
            this._toolBar = new System.Windows.Forms.ToolStrip();
            this._buttonSave = new MyLib.ToolStripMyButton();
            this._buttonClose = new MyLib.ToolStripMyButton();
            this._accountGrid = new MyLib._myGrid();
            this._toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolBar
            // 
            this._toolBar.BackgroundImage = global::SMLERPGL.Resource16x16.bt03;
            this._toolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonSave,
            this._buttonClose});
            this._toolBar.Location = new System.Drawing.Point(0, 0);
            this._toolBar.Name = "_toolBar";
            this._toolBar.Size = new System.Drawing.Size(687, 25);
            this._toolBar.TabIndex = 0;
            this._toolBar.Text = "toolStrip1";
            // 
            // _buttonSave
            // 
            this._buttonSave.Image = global::SMLERPGL.Resource16x16.disk_blue;
            this._buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Padding = new System.Windows.Forms.Padding(1);
            this._buttonSave.Size = new System.Drawing.Size(121, 22);
            this._buttonSave.Text = "บันทึกทั้งหมด (F12)";
            this._buttonSave.Click += new System.EventHandler(this._buttonSave_Click);
            // 
            // _buttonClose
            // 
            this._buttonClose.Image = global::SMLERPGL.Resource16x16.error;
            this._buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Padding = new System.Windows.Forms.Padding(1);
            this._buttonClose.Size = new System.Drawing.Size(102, 22);
            this._buttonClose.Text = "ปิดหน้าจอ (Esc)";
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
            // 
            // _accountGrid
            // 
            this._accountGrid.BackColor = System.Drawing.SystemColors.Window;
            this._accountGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            this._accountGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(220)))), ((int)(((byte)(249)))));
            this._accountGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._accountGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._accountGrid.Location = new System.Drawing.Point(0, 25);
            this._accountGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._accountGrid.Name = "_accountGrid";
            this._accountGrid.Size = new System.Drawing.Size(687, 506);
            this._accountGrid.TabIndex = 1;
            this._accountGrid.TabStop = false;
            // 
            // _chartOfAccountFast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this._accountGrid);
            this.Controls.Add(this._toolBar);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_chartOfAccountFast";
            this.Size = new System.Drawing.Size(687, 531);
            this._toolBar.ResumeLayout(false);
            this._toolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _toolBar;
        private MyLib._myGrid _accountGrid;
        private MyLib.ToolStripMyButton _buttonSave;
        private MyLib.ToolStripMyButton _buttonClose;
    }
}
