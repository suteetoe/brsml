namespace MyLib._databaseManage
{
    partial class _genQueryControl
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._sourceTextBox = new System.Windows.Forms.TextBox();
            this._resultTextBox = new System.Windows.Forms.TextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this._clearButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._tableNameTextBox = new System.Windows.Forms.TextBox();
            this._columnGrid = new MyLib._myGrid();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::MyLib.Properties.Resources.bt03;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(912, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::MyLib.Properties.Resources.error1;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(56, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this._columnGrid);
            this.splitContainer1.Panel1.Controls.Add(this._tableNameTextBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(912, 655);
            this.splitContainer1.SplitterDistance = 304;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._sourceTextBox);
            this.splitContainer2.Panel1.Controls.Add(this.toolStrip3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._resultTextBox);
            this.splitContainer2.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer2.Size = new System.Drawing.Size(604, 655);
            this.splitContainer2.SplitterDistance = 272;
            this.splitContainer2.TabIndex = 0;
            // 
            // _sourceTextBox
            // 
            this._sourceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._sourceTextBox.Location = new System.Drawing.Point(0, 25);
            this._sourceTextBox.MaxLength = 627670000;
            this._sourceTextBox.Multiline = true;
            this._sourceTextBox.Name = "_sourceTextBox";
            this._sourceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._sourceTextBox.Size = new System.Drawing.Size(602, 245);
            this._sourceTextBox.TabIndex = 0;
            // 
            // _resultTextBox
            // 
            this._resultTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultTextBox.Location = new System.Drawing.Point(0, 25);
            this._resultTextBox.MaxLength = 627670000;
            this._resultTextBox.Multiline = true;
            this._resultTextBox.Name = "_resultTextBox";
            this._resultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._resultTextBox.Size = new System.Drawing.Size(602, 352);
            this._resultTextBox.TabIndex = 1;
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackgroundImage = global::MyLib.Properties.Resources.bt03;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(602, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::MyLib.Properties.Resources.flash;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(109, 22);
            this.toolStripButton1.Text = "Generate Query";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStrip3
            // 
            this.toolStrip3.BackgroundImage = global::MyLib.Properties.Resources.bt03;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSeparator1,
            this._clearButton});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(602, 25);
            this.toolStrip3.TabIndex = 3;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // _clearButton
            // 
            this._clearButton.Image = global::MyLib.Properties.Resources.flash;
            this._clearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._clearButton.Name = "_clearButton";
            this._clearButton.Size = new System.Drawing.Size(54, 22);
            this._clearButton.Text = "Clear";
            this._clearButton.Click += new System.EventHandler(this._clearButton_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel1.Text = "Source";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _tableNameTextBox
            // 
            this._tableNameTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this._tableNameTextBox.Location = new System.Drawing.Point(0, 0);
            this._tableNameTextBox.Name = "_tableNameTextBox";
            this._tableNameTextBox.Size = new System.Drawing.Size(302, 22);
            this._tableNameTextBox.TabIndex = 3;
            // 
            // _columnGrid
            // 
            this._columnGrid._extraWordShow = true;
            this._columnGrid._selectRow = -1;
            this._columnGrid.BackColor = System.Drawing.SystemColors.Window;
            this._columnGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._columnGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._columnGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._columnGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._columnGrid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._columnGrid.Location = new System.Drawing.Point(0, 22);
            this._columnGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._columnGrid.Name = "_columnGrid";
            this._columnGrid.Size = new System.Drawing.Size(302, 631);
            this._columnGrid.TabIndex = 0;
            this._columnGrid.TabStop = false;
            // 
            // _genQueryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_genQueryControl";
            this.Size = new System.Drawing.Size(912, 680);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _closeButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private _myGrid _columnGrid;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox _sourceTextBox;
        private System.Windows.Forms.TextBox _resultTextBox;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _clearButton;
        private System.Windows.Forms.TextBox _tableNameTextBox;
    }
}
