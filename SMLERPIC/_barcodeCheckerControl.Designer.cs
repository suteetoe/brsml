namespace SMLERPIC
{
    partial class _barcodeCheckerControl
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._importButton = new System.Windows.Forms.ToolStripButton();
            this._processButton = new System.Windows.Forms.ToolStripButton();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this._errorTextBox = new System.Windows.Forms.TextBox();
            this._grid = new MyLib._myGrid();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._importButton,
            this._processButton,
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(969, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _importButton
            // 
            this._importButton.Image = global::SMLERPIC.Properties.Resources.folder_into;
            this._importButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._importButton.Name = "_importButton";
            this._importButton.Size = new System.Drawing.Size(165, 22);
            this._importButton.Text = "Import Data from Text File";
            this._importButton.Click += new System.EventHandler(this._importButton_Click);
            // 
            // _processButton
            // 
            this._processButton.Image = global::SMLERPIC.Properties.Resources.flash;
            this._processButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(67, 22);
            this._processButton.Text = "Process";
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPIC.Properties.Resources.error1;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(56, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _errorTextBox
            // 
            this._errorTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this._errorTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._errorTextBox.Location = new System.Drawing.Point(0, 544);
            this._errorTextBox.Multiline = true;
            this._errorTextBox.Name = "_errorTextBox";
            this._errorTextBox.Size = new System.Drawing.Size(969, 140);
            this._errorTextBox.TabIndex = 2;
            // 
            // _grid
            // 
            this._grid._extraWordShow = true;
            this._grid._selectRow = -1;
            this._grid.BackColor = System.Drawing.SystemColors.Window;
            this._grid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._grid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._grid.Location = new System.Drawing.Point(0, 25);
            this._grid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._grid.Name = "_grid";
            this._grid.Size = new System.Drawing.Size(969, 519);
            this._grid.TabIndex = 1;
            this._grid.TabStop = false;
            // 
            // _barcodeCheckerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._grid);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this._errorTextBox);
            this.Name = "_barcodeCheckerControl";
            this.Size = new System.Drawing.Size(969, 684);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _importButton;
        private System.Windows.Forms.ToolStripButton _processButton;
        private System.Windows.Forms.ToolStripButton _closeButton;
        private MyLib._myGrid _grid;
        private System.Windows.Forms.TextBox _errorTextBox;
    }
}
