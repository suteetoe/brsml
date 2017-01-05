namespace SMLPOSControl
{
    partial class _checkerKitchen
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
            this.label1 = new System.Windows.Forms.Label();
            this._barcodeTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._webBrowser = new System.Windows.Forms.WebBrowser();
            this._waitGrid = new MyLib._myGrid();
            this.label3 = new System.Windows.Forms.Label();
            this._lastGrid = new MyLib._myGrid();
            this.label2 = new System.Windows.Forms.Label();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this._timer_focus = new System.Windows.Forms.Timer(this.components);
            this._timer_warning = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(5, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "BARCODE :";
            // 
            // _barcodeTextBox
            // 
            this._barcodeTextBox.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._barcodeTextBox.Location = new System.Drawing.Point(130, 11);
            this._barcodeTextBox.Name = "_barcodeTextBox";
            this._barcodeTextBox.Size = new System.Drawing.Size(353, 33);
            this._barcodeTextBox.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this._barcodeTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(994, 52);
            this.panel1.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(491, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(340, 19);
            this.label4.TabIndex = 2;
            this.label4.Text = "(ยิง BARCODE หรือป้อนเลขบรรทัด แล้วกด Enter)";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._lastGrid);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(994, 662);
            this.splitContainer1.SplitterDistance = 379;
            this.splitContainer1.TabIndex = 4;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 52);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._webBrowser);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._waitGrid);
            this.splitContainer2.Panel2.Controls.Add(this.label3);
            this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(5);
            this.splitContainer2.Size = new System.Drawing.Size(994, 327);
            this.splitContainer2.SplitterDistance = 71;
            this.splitContainer2.TabIndex = 4;
            // 
            // _webBrowser
            // 
            this._webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this._webBrowser.Location = new System.Drawing.Point(0, 0);
            this._webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this._webBrowser.Name = "_webBrowser";
            this._webBrowser.Size = new System.Drawing.Size(992, 69);
            this._webBrowser.TabIndex = 0;
            // 
            // _waitGrid
            // 
            this._waitGrid._extraWordShow = true;
            this._waitGrid._selectRow = -1;
            this._waitGrid.BackColor = System.Drawing.SystemColors.Window;
            this._waitGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._waitGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._waitGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._waitGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._waitGrid.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._waitGrid.IsEdit = false;
            this._waitGrid.Location = new System.Drawing.Point(5, 24);
            this._waitGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._waitGrid.Name = "_waitGrid";
            this._waitGrid.Size = new System.Drawing.Size(982, 221);
            this._waitGrid.TabIndex = 2;
            this._waitGrid.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(5, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "รายการอาหารรอ";
            // 
            // _lastGrid
            // 
            this._lastGrid._extraWordShow = true;
            this._lastGrid._selectRow = -1;
            this._lastGrid.BackColor = System.Drawing.SystemColors.Window;
            this._lastGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._lastGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._lastGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._lastGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lastGrid.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lastGrid.IsEdit = false;
            this._lastGrid.Location = new System.Drawing.Point(0, 19);
            this._lastGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._lastGrid.Name = "_lastGrid";
            this._lastGrid.Size = new System.Drawing.Size(992, 258);
            this._lastGrid.TabIndex = 0;
            this._lastGrid.TabStop = false;
            this._lastGrid.Load += new System.EventHandler(this._lastGrid_Load);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "รายการอาหารออกล่าสุด";
            // 
            // _toolStrip
            // 
            this._toolStrip.BackgroundImage = global::SMLPOSControl.Properties.Resources.bt03;
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._closeButton});
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(994, 25);
            this._toolStrip.TabIndex = 2;
            this._toolStrip.Text = "toolStrip1";
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLPOSControl.Properties.Resources.error1;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(56, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _timer
            // 
            this._timer.Enabled = true;
            this._timer.Interval = 10000;
            // 
            // _timer_focus
            // 
            this._timer_focus.Enabled = true;
            this._timer_focus.Interval = 1000;
            this._timer_focus.Tick += new System.EventHandler(this._timer_focus_Tick);
            // 
            // _timer_warning
            // 
            this._timer_warning.Enabled = true;
            this._timer_warning.Interval = 61200;
            this._timer_warning.Tick += new System.EventHandler(this._timer_warning_Tick);
            // 
            // _checkerKitchen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this._toolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_checkerKitchen";
            this.Size = new System.Drawing.Size(994, 687);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _barcodeTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.WebBrowser _webBrowser;
        private MyLib._myGrid _lastGrid;
        private System.Windows.Forms.Label label2;
        private MyLib._myGrid _waitGrid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.ToolStripButton _closeButton;
        private System.Windows.Forms.Timer _timer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer _timer_focus;
        private System.Windows.Forms.Timer _timer_warning;
    }
}
