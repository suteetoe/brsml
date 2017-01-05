namespace SMLPOSControl
{
    partial class _checkerDeviceConfig
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._grid = new MyLib._myGrid();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._saveButton = new MyLib.VistaButton();
            this._closeButton = new MyLib.VistaButton();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _grid
            // 
            this._grid._extraWordShow = true;
            this._grid._selectRow = -1;
            this._grid.BackColor = System.Drawing.SystemColors.Window;
            this._grid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._grid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._grid.Location = new System.Drawing.Point(0, 0);
            this._grid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._grid.Name = "_grid";
            this._grid.Size = new System.Drawing.Size(550, 480);
            this._grid.TabIndex = 0;
            this._grid.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this._closeButton);
            this.flowLayoutPanel1.Controls.Add(this._saveButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 450);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(550, 30);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // _saveButton
            // 
            this._saveButton._drawNewMethod = false;
            this._saveButton.AutoSize = true;
            this._saveButton.BackColor = System.Drawing.Color.Transparent;
            this._saveButton.ButtonText = "Save";
            this._saveButton.Location = new System.Drawing.Point(415, 3);
            this._saveButton.myImage = global::SMLPOSControl.Properties.Resources.disk_blue;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(62, 24);
            this._saveButton.TabIndex = 0;
            this._saveButton.Text = "Save";
            this._saveButton.UseVisualStyleBackColor = false;
            // 
            // _closeButton
            // 
            this._closeButton._drawNewMethod = false;
            this._closeButton.AutoSize = true;
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.ButtonText = "Close";
            this._closeButton.Location = new System.Drawing.Point(483, 3);
            this._closeButton.myImage = global::SMLPOSControl.Properties.Resources.error1;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(64, 24);
            this._closeButton.TabIndex = 1;
            this._closeButton.Text = "Close";
            this._closeButton.UseVisualStyleBackColor = false;
            // 
            // _checkerDeviceConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 480);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this._grid);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_checkerDeviceConfig";
            this.Text = "Checker Config";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myGrid _grid;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MyLib.VistaButton _closeButton;
        private MyLib.VistaButton _saveButton;
    }
}