namespace MyLib
{
    partial class _myGridImportFromTextFileForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._dataGridView = new System.Windows.Forms.DataGridView();
            this._mapFieldView = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._mapFieldView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(877, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
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
            this._fromClipboardButton.Image = global::MyLib.Properties.Resources.paste;
            this._fromClipboardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._fromClipboardButton.Name = "_fromClipboardButton";
            this._fromClipboardButton.Size = new System.Drawing.Size(110, 22);
            this._fromClipboardButton.Text = "From Clipboard";
            this._fromClipboardButton.Click += new System.EventHandler(this._fromClipboardButton_Click);
            // 
            // _openTextFileButton
            // 
            this._openTextFileButton.Image = global::MyLib.Properties.Resources.folder_into;
            this._openTextFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._openTextFileButton.Name = "_openTextFileButton";
            this._openTextFileButton.Size = new System.Drawing.Size(101, 22);
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
            this._importButton.Image = global::MyLib.Properties.Resources.flash1;
            this._importButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._importButton.Name = "_importButton";
            this._importButton.Size = new System.Drawing.Size(91, 22);
            this._importButton.Text = "Import Now";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._dataGridView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._mapFieldView);
            this.splitContainer1.Size = new System.Drawing.Size(877, 467);
            this.splitContainer1.SplitterDistance = 602;
            this.splitContainer1.TabIndex = 1;
            // 
            // _dataGridView
            // 
            this._dataGridView.AllowUserToAddRows = false;
            this._dataGridView.AllowUserToDeleteRows = false;
            this._dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGridView.Location = new System.Drawing.Point(0, 0);
            this._dataGridView.Name = "_dataGridView";
            this._dataGridView.ReadOnly = true;
            this._dataGridView.Size = new System.Drawing.Size(600, 465);
            this._dataGridView.TabIndex = 0;
            // 
            // _mapFieldView
            // 
            this._mapFieldView.AllowUserToAddRows = false;
            this._mapFieldView.AllowUserToDeleteRows = false;
            this._mapFieldView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._mapFieldView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._mapFieldView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mapFieldView.Location = new System.Drawing.Point(0, 0);
            this._mapFieldView.Name = "_mapFieldView";
            this._mapFieldView.Size = new System.Drawing.Size(269, 465);
            this._mapFieldView.TabIndex = 1;
            // 
            // _myGridImportFromTextFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(877, 492);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_myGridImportFromTextFileForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._mapFieldView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _openTextFileButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.DataGridView _mapFieldView;
        public System.Windows.Forms.ToolStripButton _importButton;
        public System.Windows.Forms.DataGridView _dataGridView;
        private System.Windows.Forms.ToolStripComboBox _splitComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox _encodingComboBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton _fromClipboardButton;
    }
}