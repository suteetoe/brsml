namespace SMLERPGL
{
    partial class _transProcessUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_transProcessUserControl));
            this._conditionScreen = new MyLib._myScreen();
            this._panel = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._transTypeGrid = new MyLib._myGrid();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.panel1 = new MyLib._myPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._resultGrid = new MyLib._myGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._selectAllButton = new System.Windows.Forms.ToolStripButton();
            this._removeSelectAllButton = new System.Windows.Forms.ToolStripButton();
            this._processButton = new System.Windows.Forms.Button();
            this._stopButton = new System.Windows.Forms.Button();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this._panel.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _conditionScreen
            // 
            this._conditionScreen._isChange = false;
            this._conditionScreen.AutoSize = true;
            this._conditionScreen.BackColor = System.Drawing.Color.Transparent;
            this._conditionScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._conditionScreen.Location = new System.Drawing.Point(0, 0);
            this._conditionScreen.Name = "_conditionScreen";
            this._conditionScreen.Padding = new System.Windows.Forms.Padding(10);
            this._conditionScreen.Size = new System.Drawing.Size(389, 20);
            this._conditionScreen.TabIndex = 1;
            // 
            // _panel
            // 
            this._panel.Controls.Add(this.splitContainer1);
            this._panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panel.Location = new System.Drawing.Point(0, 25);
            this._panel.Name = "_panel";
            this._panel.Size = new System.Drawing.Size(800, 596);
            this._panel.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._transTypeGrid);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip2);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._resultGrid);
            this.splitContainer1.Size = new System.Drawing.Size(800, 596);
            this.splitContainer1.SplitterDistance = 389;
            this.splitContainer1.TabIndex = 0;
            // 
            // _transTypeGrid
            // 
            this._transTypeGrid._extraWordShow = true;
            this._transTypeGrid._selectRow = -1;
            this._transTypeGrid.BackColor = System.Drawing.SystemColors.Window;
            this._transTypeGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._transTypeGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._transTypeGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._transTypeGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._transTypeGrid.Location = new System.Drawing.Point(0, 49);
            this._transTypeGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._transTypeGrid.Name = "_transTypeGrid";
            this._transTypeGrid.Size = new System.Drawing.Size(389, 522);
            this._transTypeGrid.TabIndex = 0;
            this._transTypeGrid.TabStop = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._selectAllButton,
            this._removeSelectAllButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 571);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(389, 25);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // panel1
            // 
            this.panel1._switchTabAuto = false;
            this.panel1.AutoSize = true;
            this.panel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this._conditionScreen);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.CornerPicture = null;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(389, 49);
            this.panel1.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this._processButton);
            this.flowLayoutPanel1.Controls.Add(this._stopButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 20);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(389, 29);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // _resultGrid
            // 
            this._resultGrid._extraWordShow = true;
            this._resultGrid._selectRow = -1;
            this._resultGrid.BackColor = System.Drawing.SystemColors.Window;
            this._resultGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._resultGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._resultGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._resultGrid.IsEdit = false;
            this._resultGrid.Location = new System.Drawing.Point(0, 0);
            this._resultGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._resultGrid.Name = "_resultGrid";
            this._resultGrid.Size = new System.Drawing.Size(407, 596);
            this._resultGrid.TabIndex = 0;
            this._resultGrid.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _selectAllButton
            // 
            this._selectAllButton.Image = global::SMLERPGL.Properties.Resources.checks;
            this._selectAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectAllButton.Name = "_selectAllButton";
            this._selectAllButton.Size = new System.Drawing.Size(75, 22);
            this._selectAllButton.Text = "Select All";
            this._selectAllButton.Click += new System.EventHandler(this._selectAllButton_Click);
            // 
            // _removeSelectAllButton
            // 
            this._removeSelectAllButton.Image = ((System.Drawing.Image)(resources.GetObject("_removeSelectAllButton.Image")));
            this._removeSelectAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._removeSelectAllButton.Name = "_removeSelectAllButton";
            this._removeSelectAllButton.Size = new System.Drawing.Size(121, 22);
            this._removeSelectAllButton.Text = "Remove Select All";
            this._removeSelectAllButton.Click += new System.EventHandler(this._removeSelectAllButton_Click);
            // 
            // _processButton
            // 
            this._processButton.AutoSize = true;
            this._processButton.Image = global::SMLERPGL.Properties.Resources.flash;
            this._processButton.Location = new System.Drawing.Point(3, 3);
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(75, 23);
            this._processButton.TabIndex = 0;
            this._processButton.Text = "Process";
            this._processButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._processButton.UseVisualStyleBackColor = true;
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _stopButton
            // 
            this._stopButton.AutoSize = true;
            this._stopButton.Image = global::SMLERPGL.Properties.Resources.stop;
            this._stopButton.Location = new System.Drawing.Point(84, 3);
            this._stopButton.Name = "_stopButton";
            this._stopButton.Size = new System.Drawing.Size(96, 23);
            this._stopButton.TabIndex = 1;
            this._stopButton.Text = "Stop Process";
            this._stopButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._stopButton.UseVisualStyleBackColor = true;
            this._stopButton.Click += new System.EventHandler(this._stopButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPGL.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(56, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _timer
            // 
            this._timer.Interval = 1000;
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // _transProcessUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._panel);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_transProcessUserControl";
            this.Size = new System.Drawing.Size(800, 621);
            this._panel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myScreen _conditionScreen;
        private System.Windows.Forms.Panel _panel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MyLib._myGrid _transTypeGrid;
        private MyLib._myGrid _resultGrid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _closeButton;
        private MyLib._myPanel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button _processButton;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton _selectAllButton;
        private System.Windows.Forms.ToolStripButton _removeSelectAllButton;
        private System.Windows.Forms.Button _stopButton;
        private System.Windows.Forms.Timer _timer;

    }
}
