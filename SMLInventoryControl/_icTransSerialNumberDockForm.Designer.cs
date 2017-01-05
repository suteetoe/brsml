namespace SMLInventoryControl
{
    partial class _icTransSerialNumberDockForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._saveButton = new System.Windows.Forms.ToolStripButton();
            this._cancelButton = new System.Windows.Forms.ToolStripButton();
            this._dock = new Crom.Controls.Docking.DockContainer();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._cancelButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(784, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLInventoryControl.Properties.Resources.flash;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(51, 22);
            this._saveButton.Text = "Save";
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.Image = global::SMLInventoryControl.Properties.Resources.error1;
            this._cancelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(63, 22);
            this._cancelButton.Text = "Cancel";
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _dock
            // 
            this._dock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(118)))), ((int)(((byte)(118)))));
            this._dock.CanMoveByMouseFilledForms = true;
            this._dock.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dock.Location = new System.Drawing.Point(0, 25);
            this._dock.Name = "_dock";
            this._dock.Size = new System.Drawing.Size(784, 637);
            this._dock.TabIndex = 1;
            this._dock.TitleBarGradientColor1 = System.Drawing.SystemColors.Control;
            this._dock.TitleBarGradientColor2 = System.Drawing.Color.White;
            this._dock.TitleBarGradientSelectedColor1 = System.Drawing.Color.DarkGray;
            this._dock.TitleBarGradientSelectedColor2 = System.Drawing.Color.White;
            this._dock.TitleBarTextColor = System.Drawing.Color.Black;
            // 
            // _icTransSerialNumberDockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Controls.Add(this._dock);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icTransSerialNumberDockForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_icTransSerialNumberDockForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private Crom.Controls.Docking.DockContainer _dock;
        private System.Windows.Forms.ToolStripButton _saveButton;
        private System.Windows.Forms.ToolStripButton _cancelButton;
    }
}