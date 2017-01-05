namespace SMLERPTemplate
{
    partial class _editGroupMenuForm
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
            this._textBox = new System.Windows.Forms.TextBox();
            this._saveButton = new System.Windows.Forms.ToolStripButton();
            this._cancelButton = new System.Windows.Forms.ToolStripButton();
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
            this.toolStrip1.Size = new System.Drawing.Size(284, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _textBox
            // 
            this._textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._textBox.Location = new System.Drawing.Point(0, 25);
            this._textBox.Multiline = true;
            this._textBox.Name = "_textBox";
            this._textBox.Size = new System.Drawing.Size(284, 237);
            this._textBox.TabIndex = 1;
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLERPTemplate.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(51, 22);
            this._saveButton.Text = "Save";
            // 
            // _cancelButton
            // 
            this._cancelButton.Image = global::SMLERPTemplate.Properties.Resources.error;
            this._cancelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(63, 22);
            this._cancelButton.Text = "Cancel";
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _editGroupMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.ControlBox = false;
            this.Controls.Add(this._textBox);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_editGroupMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Group";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _cancelButton;
        public System.Windows.Forms.ToolStripButton _saveButton;
        public System.Windows.Forms.TextBox _textBox;
    }
}