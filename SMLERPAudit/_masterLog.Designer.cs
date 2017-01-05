namespace SMLERPAudit
{
    partial class _masterLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_masterLog));
            this._processButton = new System.Windows.Forms.ToolStripButton();
            this._dataGrid = new MyLib._myGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._conditionScreen = new SMLERPAudit._conditionScreenClass();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _processButton
            // 
            this._processButton.Image = ((System.Drawing.Image)(resources.GetObject("_processButton.Image")));
            this._processButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(67, 22);
            this._processButton.Text = "Process";
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _dataGrid
            // 
            this._dataGrid._extraWordShow = true;
            this._dataGrid._selectRow = -1;
            this._dataGrid.BackColor = System.Drawing.SystemColors.Window;
            this._dataGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._dataGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._dataGrid.IsEdit = false;
            this._dataGrid.Location = new System.Drawing.Point(0, 69);
            this._dataGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._dataGrid.Name = "_dataGrid";
            this._dataGrid.Size = new System.Drawing.Size(975, 777);
            this._dataGrid.TabIndex = 8;
            this._dataGrid.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._processButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 44);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(975, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _conditionScreen
            // 
            this._conditionScreen._isChange = false;
            this._conditionScreen.AutoSize = true;
            this._conditionScreen.BackColor = System.Drawing.Color.Transparent;
            this._conditionScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this._conditionScreen.Location = new System.Drawing.Point(0, 0);
            this._conditionScreen.Name = "_conditionScreen";
            this._conditionScreen.Size = new System.Drawing.Size(975, 44);
            this._conditionScreen.TabIndex = 6;
            // 
            // _masterLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._dataGrid);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this._conditionScreen);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_masterLog";
            this.Size = new System.Drawing.Size(975, 846);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton _processButton;
        private MyLib._myGrid _dataGrid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private _conditionScreenClass _conditionScreen;
    }
}
