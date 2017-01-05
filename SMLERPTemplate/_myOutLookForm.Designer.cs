namespace SMLERPTemplate
{
    partial class _myOutLookForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_myOutLookForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this._listView = new System.Windows.Forms.ListView();
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._editGroupButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._addFastReportToMenuButton = new System.Windows.Forms.ToolStripButton();
            this._deleteMenuButton = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this._listView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(298, 552);
            this.panel1.TabIndex = 1;
            // 
            // _listView
            // 
            this._listView.AllowDrop = true;
            this._listView.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this._listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listView.LargeImageList = this._imageList;
            this._listView.Location = new System.Drawing.Point(0, 0);
            this._listView.MultiSelect = false;
            this._listView.Name = "_listView";
            this._listView.Size = new System.Drawing.Size(296, 550);
            this._listView.TabIndex = 0;
            this._listView.UseCompatibleStateImageBehavior = false;
            // 
            // _imageList
            // 
            this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
            this._imageList.TransparentColor = System.Drawing.Color.Transparent;
            this._imageList.Images.SetKeyName(0, "window.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._editGroupButton,
            this.toolStripSeparator1,
            this._addFastReportToMenuButton,
            this._deleteMenuButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(298, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _editGroupButton
            // 
            this._editGroupButton.Image = global::SMLERPTemplate.Properties.Resources.add;
            this._editGroupButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._editGroupButton.Name = "_editGroupButton";
            this._editGroupButton.Size = new System.Drawing.Size(83, 22);
            this._editGroupButton.Text = "Edit Group";
            this._editGroupButton.Click += new System.EventHandler(this._editGroupButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _addFastReportToMenuButton
            // 
            this._addFastReportToMenuButton.Image = global::SMLERPTemplate.Properties.Resources.folder;
            this._addFastReportToMenuButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._addFastReportToMenuButton.Name = "_addFastReportToMenuButton";
            this._addFastReportToMenuButton.Size = new System.Drawing.Size(86, 22);
            this._addFastReportToMenuButton.Text = "Fast Report";
            this._addFastReportToMenuButton.Click += new System.EventHandler(this._addMenuButton_Click);
            // 
            // _deleteMenuButton
            // 
            this._deleteMenuButton.Image = global::SMLERPTemplate.Properties.Resources.delete;
            this._deleteMenuButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._deleteMenuButton.Name = "_deleteMenuButton";
            this._deleteMenuButton.Size = new System.Drawing.Size(94, 22);
            this._deleteMenuButton.Text = "Delete Menu";
            this._deleteMenuButton.Click += new System.EventHandler(this._deleteMenuButton_Click);
            // 
            // _myOutLookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 577);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_myOutLookForm";
            this.Text = "Menu";
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ListView _listView;
        private System.Windows.Forms.ImageList _imageList;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripButton _editGroupButton;
        public System.Windows.Forms.ToolStripButton _deleteMenuButton;
        public System.Windows.Forms.ToolStripButton _addFastReportToMenuButton;
    }
}