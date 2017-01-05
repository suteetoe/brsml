namespace Calendar
{
    partial class _calendar
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
            this._calendarPanel = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._yearMonthLabel = new MyLib._myShadowLabel(this.components);
            this.panel1 = new MyLib._myPanel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._refreshButton = new System.Windows.Forms.ToolStripButton();
            this._prevMonthButton = new System.Windows.Forms.ToolStripButton();
            this._nextMonthButton = new System.Windows.Forms.ToolStripButton();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _calendarPanel
            // 
            this._calendarPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._calendarPanel.Location = new System.Drawing.Point(0, 64);
            this._calendarPanel.Name = "_calendarPanel";
            this._calendarPanel.Size = new System.Drawing.Size(1071, 694);
            this._calendarPanel.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._refreshButton,
            this.toolStripSeparator1,
            this._prevMonthButton,
            this.toolStripSeparator2,
            this._nextMonthButton,
            this.toolStripSeparator3,
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1071, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _yearMonthLabel
            // 
            this._yearMonthLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._yearMonthLabel.Angle = 0F;
            this._yearMonthLabel.AutoSize = true;
            this._yearMonthLabel.BackColor = System.Drawing.Color.Transparent;
            this._yearMonthLabel.DrawGradient = false;
            this._yearMonthLabel.EndColor = System.Drawing.Color.LightSkyBlue;
            this._yearMonthLabel.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._yearMonthLabel.ForeColor = System.Drawing.Color.LightSkyBlue;
            this._yearMonthLabel.Location = new System.Drawing.Point(7, 7);
            this._yearMonthLabel.Name = "_yearMonthLabel";
            this._yearMonthLabel.ShadowColor = System.Drawing.Color.Black;
            this._yearMonthLabel.Size = new System.Drawing.Size(210, 25);
            this._yearMonthLabel.StartColor = System.Drawing.Color.White;
            this._yearMonthLabel.TabIndex = 0;
            this._yearMonthLabel.Text = "_myShadowLabel1";
            this._yearMonthLabel.XOffset = 1F;
            this._yearMonthLabel.YOffset = 1F;
            // 
            // panel1
            // 
            this.panel1._switchTabAuto = false;
            this.panel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this._yearMonthLabel);
            this.panel1.CornerPicture = null;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1071, 39);
            this.panel1.TabIndex = 0;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // _refreshButton
            // 
            this._refreshButton.Image = global::Calendar.Properties.Resources.refresh;
            this._refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._refreshButton.Name = "_refreshButton";
            this._refreshButton.Size = new System.Drawing.Size(66, 22);
            this._refreshButton.Text = "Refresh";
            this._refreshButton.Click += new System.EventHandler(this._refreshButton_Click);
            // 
            // _prevMonthButton
            // 
            this._prevMonthButton.Image = global::Calendar.Properties.Resources.nav_left_blue;
            this._prevMonthButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._prevMonthButton.Name = "_prevMonthButton";
            this._prevMonthButton.Size = new System.Drawing.Size(89, 22);
            this._prevMonthButton.Text = "Prev Month";
            this._prevMonthButton.Click += new System.EventHandler(this._prevMonthButton_Click);
            // 
            // _nextMonthButton
            // 
            this._nextMonthButton.Image = global::Calendar.Properties.Resources.nav_right_blue;
            this._nextMonthButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._nextMonthButton.Name = "_nextMonthButton";
            this._nextMonthButton.Size = new System.Drawing.Size(90, 22);
            this._nextMonthButton.Text = "Next Month";
            this._nextMonthButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this._nextMonthButton.Click += new System.EventHandler(this._nextMonthButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::Calendar.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(56, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _calendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._calendarPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_calendar";
            this.Size = new System.Drawing.Size(1071, 758);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _prevMonthButton;
        private System.Windows.Forms.ToolStripButton _nextMonthButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private MyLib._myShadowLabel _yearMonthLabel;
        private MyLib._myPanel panel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripButton _refreshButton;
        public System.Windows.Forms.Panel _calendarPanel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripButton _closeButton;
    }
}
