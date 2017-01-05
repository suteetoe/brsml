namespace SMLReport._design
{
    partial class _linkPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_linkPanel));
            this._toolBar = new System.Windows.Forms.ToolStrip();
            this._label = new System.Windows.Forms.ToolStripButton();
            this._joinComboBox = new System.Windows.Forms.ToolStripComboBox();
            this._remove = new System.Windows.Forms.ToolStripButton();
            this._listBox = new System.Windows.Forms.ListBox();
            this._toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolBar
            // 
            this._toolBar.AutoSize = false;
            this._toolBar.Dock = System.Windows.Forms.DockStyle.None;
            this._toolBar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._toolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._label,
            this._joinComboBox,
            this._remove});
            this._toolBar.Location = new System.Drawing.Point(0, 0);
            this._toolBar.Name = "_toolBar";
            this._toolBar.Padding = new System.Windows.Forms.Padding(0);
            this._toolBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this._toolBar.Size = new System.Drawing.Size(118, 25);
            this._toolBar.TabIndex = 0;
            this._toolBar.Text = "Tool bar";
            // 
            // _label
            // 
            this._label.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._label.Image = ((System.Drawing.Image)(resources.GetObject("_label.Image")));
            this._label.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._label.Name = "_label";
            this._label.Size = new System.Drawing.Size(36, 22);
            this._label.Text = "Label";
            this._label.Click += new System.EventHandler(this._label_Click);
            // 
            // _joinComboBox
            // 
            this._joinComboBox.AutoSize = false;
            this._joinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._joinComboBox.Items.AddRange(new object[] {
            "INNER",
            "LEFT",
            "RIGHT",
            "FULL"});
            this._joinComboBox.Name = "_joinComboBox";
            this._joinComboBox.Size = new System.Drawing.Size(60, 21);
            // 
            // _remove
            // 
            this._remove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._remove.Image = global::SMLReport.Resource16x16.garbage_empty;
            this._remove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._remove.Name = "_remove";
            this._remove.Size = new System.Drawing.Size(23, 20);
            this._remove.Text = "Remove";
            this._remove.Click += new System.EventHandler(this._remove_Click);
            // 
            // _listBox
            // 
            this._listBox.FormattingEnabled = true;
            this._listBox.Location = new System.Drawing.Point(2, 26);
            this._listBox.Name = "_listBox";
            this._listBox.Size = new System.Drawing.Size(221, 121);
            this._listBox.TabIndex = 1;
            // 
            // _linkPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this._listBox);
            this.Controls.Add(this._toolBar);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "_linkPanel";
            this.Size = new System.Drawing.Size(364, 150);
            this.Load += new System.EventHandler(this._linkPanel_Load);
            this._toolBar.ResumeLayout(false);
            this._toolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip _toolBar;
        private System.Windows.Forms.ToolStripButton _remove;
        public System.Windows.Forms.ListBox _listBox;
        private System.Windows.Forms.ToolStripButton _label;
        public System.Windows.Forms.ToolStripComboBox _joinComboBox;
    }
}
