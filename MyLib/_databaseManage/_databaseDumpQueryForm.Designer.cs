namespace MyLib._databaseManage
{
    partial class _databaseDumpQueryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_databaseDumpQueryForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this._selectTableTab = new System.Windows.Forms.TabPage();
            this._queryResultTab = new System.Windows.Forms.TabPage();
            this._queryResult = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._genButton = new System.Windows.Forms.ToolStripButton();
            this._saveGzButton = new System.Windows.Forms.ToolStripButton();
            this._myPanel1 = new MyLib._myPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._myGridTable = new MyLib._myGrid();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this._selectAll = new System.Windows.Forms.ToolStripButton();
            this._selectNone = new System.Windows.Forms.ToolStripButton();
            this._myGridField = new MyLib._myGrid();
            this.tabControl1.SuspendLayout();
            this._selectTableTab.SuspendLayout();
            this._queryResultTab.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this._selectTableTab);
            this.tabControl1.Controls.Add(this._queryResultTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1102, 781);
            this.tabControl1.TabIndex = 0;
            // 
            // _selectTableTab
            // 
            this._selectTableTab.Controls.Add(this._myPanel1);
            this._selectTableTab.Location = new System.Drawing.Point(4, 22);
            this._selectTableTab.Name = "_selectTableTab";
            this._selectTableTab.Padding = new System.Windows.Forms.Padding(3);
            this._selectTableTab.Size = new System.Drawing.Size(1094, 755);
            this._selectTableTab.TabIndex = 0;
            this._selectTableTab.Text = "Select Table & Field";
            this._selectTableTab.UseVisualStyleBackColor = true;
            // 
            // _queryResultTab
            // 
            this._queryResultTab.Controls.Add(this._queryResult);
            this._queryResultTab.Controls.Add(this.toolStrip1);
            this._queryResultTab.Location = new System.Drawing.Point(4, 22);
            this._queryResultTab.Name = "_queryResultTab";
            this._queryResultTab.Padding = new System.Windows.Forms.Padding(3);
            this._queryResultTab.Size = new System.Drawing.Size(1094, 755);
            this._queryResultTab.TabIndex = 1;
            this._queryResultTab.Text = "Result";
            this._queryResultTab.UseVisualStyleBackColor = true;
            // 
            // _queryResult
            // 
            this._queryResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this._queryResult.Location = new System.Drawing.Point(3, 28);
            this._queryResult.Multiline = true;
            this._queryResult.Name = "_queryResult";
            this._queryResult.Size = new System.Drawing.Size(1088, 724);
            this._queryResult.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._genButton,
            this._saveGzButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1088, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _genButton
            // 
            this._genButton.Image = ((System.Drawing.Image)(resources.GetObject("_genButton.Image")));
            this._genButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._genButton.Name = "_genButton";
            this._genButton.Size = new System.Drawing.Size(50, 22);
            this._genButton.Text = "GEN";
            this._genButton.Click += new System.EventHandler(this._genButton_Click);
            // 
            // _saveGzButton
            // 
            this._saveGzButton.Image = ((System.Drawing.Image)(resources.GetObject("_saveGzButton.Image")));
            this._saveGzButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveGzButton.Name = "_saveGzButton";
            this._saveGzButton.Size = new System.Drawing.Size(69, 22);
            this._saveGzButton.Text = "Save GZ";
            this._saveGzButton.Click += new System.EventHandler(this._saveGzButton_Click);
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this.splitContainer1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(3, 3);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.ShowLineBackground = true;
            this._myPanel1.Size = new System.Drawing.Size(1088, 749);
            this._myPanel1.TabIndex = 11;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._myGridTable);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._myGridField);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer1.Size = new System.Drawing.Size(1088, 749);
            this.splitContainer1.SplitterDistance = 506;
            this.splitContainer1.TabIndex = 0;
            // 
            // _myGridTable
            // 
            this._myGridTable._extraWordShow = true;
            this._myGridTable._selectRow = -1;
            this._myGridTable.BackColor = System.Drawing.SystemColors.Window;
            this._myGridTable.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._myGridTable.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._myGridTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myGridTable.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myGridTable.Location = new System.Drawing.Point(0, 0);
            this._myGridTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._myGridTable.Name = "_myGridTable";
            this._myGridTable.Size = new System.Drawing.Size(506, 749);
            this._myGridTable.TabIndex = 0;
            this._myGridTable.TabStop = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._selectAll,
            this._selectNone});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(578, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // _selectAll
            // 
            this._selectAll.Image = ((System.Drawing.Image)(resources.GetObject("_selectAll.Image")));
            this._selectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectAll.Name = "_selectAll";
            this._selectAll.Size = new System.Drawing.Size(41, 22);
            this._selectAll.Text = "All";
            this._selectAll.Click += new System.EventHandler(this._selectAll_Click);
            // 
            // _selectNone
            // 
            this._selectNone.Image = ((System.Drawing.Image)(resources.GetObject("_selectNone.Image")));
            this._selectNone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectNone.Name = "_selectNone";
            this._selectNone.Size = new System.Drawing.Size(56, 22);
            this._selectNone.Text = "None";
            this._selectNone.Click += new System.EventHandler(this._selectNone_Click);
            // 
            // _myGridField
            // 
            this._myGridField._extraWordShow = true;
            this._myGridField._selectRow = -1;
            this._myGridField.BackColor = System.Drawing.SystemColors.Window;
            this._myGridField.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._myGridField.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._myGridField.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myGridField.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myGridField.Location = new System.Drawing.Point(0, 25);
            this._myGridField.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._myGridField.Name = "_myGridField";
            this._myGridField.Size = new System.Drawing.Size(578, 724);
            this._myGridField.TabIndex = 1;
            this._myGridField.TabStop = false;
            // 
            // _databaseDumpQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 781);
            this.Controls.Add(this.tabControl1);
            this.Name = "_databaseDumpQueryForm";
            this.Text = "_databaseDumpQueryForm";
            this.tabControl1.ResumeLayout(false);
            this._selectTableTab.ResumeLayout(false);
            this._queryResultTab.ResumeLayout(false);
            this._queryResultTab.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage _selectTableTab;
        private System.Windows.Forms.TabPage _queryResultTab;
        private _myPanel _myPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private _myGrid _myGridTable;
        private _myGrid _myGridField;
        private System.Windows.Forms.TextBox _queryResult;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _genButton;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton _selectAll;
        private System.Windows.Forms.ToolStripButton _selectNone;
        private System.Windows.Forms.ToolStripButton _saveGzButton;
    }
}