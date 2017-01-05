namespace SMLInventoryControl
{
    partial class _icTransImportDataControl
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
            this._screen = new MyLib._myScreen();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this._encodingComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this._splitComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._fromClipboardButton = new System.Windows.Forms.ToolStripButton();
            this._openTextFileButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._importButton = new System.Windows.Forms.ToolStripButton();
            this._dataGridView = new System.Windows.Forms.DataGridView();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // _screen
            // 
            this._screen._isChange = false;
            this._screen.BackColor = System.Drawing.Color.Transparent;
            this._screen.Dock = System.Windows.Forms.DockStyle.Top;
            this._screen.Location = new System.Drawing.Point(0, 0);
            this._screen.Name = "_screen";
            this._screen.Size = new System.Drawing.Size(789, 10);
            this._screen.TabIndex = 0;
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this._encodingComboBox,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this._splitComboBox,
            this.toolStripSeparator1,
            this._fromClipboardButton,
            this._openTextFileButton,
            this.toolStripSeparator3,
            this._importButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 10);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(789, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(57, 22);
            this.toolStripLabel2.Text = "Encoding";
            // 
            // _encodingComboBox
            // 
            this._encodingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._encodingComboBox.Items.AddRange(new object[] {
            "Windows-874",
            "UTF-8"});
            this._encodingComboBox.Name = "_encodingComboBox";
            this._encodingComboBox.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(30, 22);
            this.toolStripLabel1.Text = "Split";
            // 
            // _splitComboBox
            // 
            this._splitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._splitComboBox.Items.AddRange(new object[] {
            "TAB",
            "COMMA"});
            this._splitComboBox.Name = "_splitComboBox";
            this._splitComboBox.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _fromClipboardButton
            // 
            this._fromClipboardButton.Image = global::SMLInventoryControl.Properties.Resources.cube_green_add;
            this._fromClipboardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._fromClipboardButton.Name = "_fromClipboardButton";
            this._fromClipboardButton.Size = new System.Drawing.Size(110, 22);
            this._fromClipboardButton.Text = "From Clipboard";
            this._fromClipboardButton.Click += new System.EventHandler(this._fromClipboardButton_Click);
            // 
            // _openTextFileButton
            // 
            this._openTextFileButton.Image = global::SMLInventoryControl.Properties.Resources.cube_yellow_new;
            this._openTextFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._openTextFileButton.Name = "_openTextFileButton";
            this._openTextFileButton.Size = new System.Drawing.Size(100, 22);
            this._openTextFileButton.Text = "From Text File";
            this._openTextFileButton.Click += new System.EventHandler(this._openTextFileButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // _importButton
            // 
            this._importButton.Image = global::SMLInventoryControl.Properties.Resources.flash;
            this._importButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._importButton.Name = "_importButton";
            this._importButton.Size = new System.Drawing.Size(91, 22);
            this._importButton.Text = "Import Now";
            this._importButton.Click += new System.EventHandler(this._importButton_Click);
            // 
            // _dataGridView
            // 
            this._dataGridView.AllowUserToAddRows = false;
            this._dataGridView.AllowUserToDeleteRows = false;
            this._dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGridView.Location = new System.Drawing.Point(0, 35);
            this._dataGridView.Name = "_dataGridView";
            this._dataGridView.ReadOnly = true;
            this._dataGridView.Size = new System.Drawing.Size(789, 589);
            this._dataGridView.TabIndex = 3;
            // 
            // _icTransImportDataControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 624);
            this.Controls.Add(this._dataGridView);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this._screen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "_icTransImportDataControl";
            this.Text = "Import";
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myScreen _screen;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox _encodingComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox _splitComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _fromClipboardButton;
        private System.Windows.Forms.ToolStripButton _openTextFileButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripButton _importButton;
        public System.Windows.Forms.DataGridView _dataGridView;
    }
}