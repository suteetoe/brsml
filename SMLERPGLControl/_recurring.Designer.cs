namespace SMLERPGLControl
{
    partial class _recurring
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
            this._toolBar = new System.Windows.Forms.ToolStrip();
            this._recurringRefreshButton = new System.Windows.Forms.ToolStripButton();
            this._recurringComboBox = new System.Windows.Forms.ToolStripComboBox();
            this._recurringSaveButton = new MyLib.ToolStripMyButton();
            this._recurringDeleteButton = new MyLib.ToolStripMyButton();
            this._toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolBar
            // 
            this._toolBar.BackColor = System.Drawing.Color.Transparent;
            this._toolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._recurringRefreshButton,
            this._recurringComboBox,
            this._recurringSaveButton,
            this._recurringDeleteButton});
            this._toolBar.Location = new System.Drawing.Point(0, 0);
            this._toolBar.Name = "_toolBar";
            this._toolBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this._toolBar.Size = new System.Drawing.Size(788, 25);
            this._toolBar.TabIndex = 19;
            this._toolBar.Text = "toolStrip1";
            // 
            // _recurringRefreshButton
            // 
            this._recurringRefreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._recurringRefreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._recurringRefreshButton.Name = "_recurringRefreshButton";
            this._recurringRefreshButton.Size = new System.Drawing.Size(23, 22);
            this._recurringRefreshButton.Text = "Refresh";
            this._recurringRefreshButton.Click += new System.EventHandler(this._recurringRefreshButton_Click);
            // 
            // _recurringComboBox
            // 
            this._recurringComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._recurringComboBox.Name = "_recurringComboBox";
            this._recurringComboBox.Size = new System.Drawing.Size(300, 25);
            this._recurringComboBox.ToolTipText = "(CTRL+R)";
            // 
            // _recurringSaveButton
            // 
            this._recurringSaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._recurringSaveButton.Name = "_recurringSaveButton";
            this._recurringSaveButton.Padding = new System.Windows.Forms.Padding(1);
            this._recurringSaveButton.ResourceName = "สร้างรูปแบบรายวัน (CTRL+I)";
            this._recurringSaveButton.Size = new System.Drawing.Size(144, 22);
            this._recurringSaveButton.Text = "สร้างรูปแบบรายวัน (CTRL+I)";
            this._recurringSaveButton.Click += new System.EventHandler(this._recurringSaveButton_Click);
            // 
            // _recurringDeleteButton
            // 
            this._recurringDeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._recurringDeleteButton.Name = "_recurringDeleteButton";
            this._recurringDeleteButton.Padding = new System.Windows.Forms.Padding(1);
            this._recurringDeleteButton.ResourceName = "ลบรูปแบบรายวัน (CTRL+K)";
            this._recurringDeleteButton.Size = new System.Drawing.Size(140, 22);
            this._recurringDeleteButton.Text = "ลบรูปแบบรายวัน (CTRL+K)";
            this._recurringDeleteButton.Click += new System.EventHandler(this._recurringDeleteButton_Click);
            // 
            // _recurring
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this._toolBar);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_recurring";
            this.Size = new System.Drawing.Size(788, 25);
            this._toolBar.ResumeLayout(false);
            this._toolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _toolBar;
        private System.Windows.Forms.ToolStripButton _recurringRefreshButton;
        private System.Windows.Forms.ToolStripComboBox _recurringComboBox;
        private MyLib.ToolStripMyButton _recurringSaveButton;
        private MyLib.ToolStripMyButton _recurringDeleteButton;
    }
}
