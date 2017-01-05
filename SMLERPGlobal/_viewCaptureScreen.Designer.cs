namespace _viewCapture
{
    partial class _viewCaptureScreen
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._userListComboBox = new System.Windows.Forms.ToolStripComboBox();
            this._prevButton = new System.Windows.Forms.ToolStripButton();
            this._nextButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._totalLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._infoLabel = new System.Windows.Forms.ToolStripLabel();
            this._pictureBox = new System.Windows.Forms.PictureBox();
            this._imageDrawListBox = new _viewCapture._imageListBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._userListComboBox,
            this._prevButton,
            this._nextButton,
            this.toolStripSeparator1,
            this._closeButton,
            this.toolStripSeparator2,
            this._totalLabel,
            this.toolStripSeparator3,
            this._infoLabel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(938, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _userListComboBox
            // 
            this._userListComboBox.Name = "_userListComboBox";
            this._userListComboBox.Size = new System.Drawing.Size(300, 25);
            // 
            // _prevButton
            // 
            this._prevButton.Image = global::_g.Properties.Resources.nav_left_blue;
            this._prevButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._prevButton.Name = "_prevButton";
            this._prevButton.Size = new System.Drawing.Size(50, 22);
            this._prevButton.Text = "Prev";
            this._prevButton.Click += new System.EventHandler(this._prevButton_Click);
            // 
            // _nextButton
            // 
            this._nextButton.Image = global::_g.Properties.Resources.nav_right_blue;
            this._nextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._nextButton.Name = "_nextButton";
            this._nextButton.Size = new System.Drawing.Size(51, 22);
            this._nextButton.Text = "Next";
            this._nextButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this._nextButton.Click += new System.EventHandler(this._nextButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::_g.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(56, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _totalLabel
            // 
            this._totalLabel.Name = "_totalLabel";
            this._totalLabel.Size = new System.Drawing.Size(49, 22);
            this._totalLabel.Text = "Records";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // _infoLabel
            // 
            this._infoLabel.Name = "_infoLabel";
            this._infoLabel.Size = new System.Drawing.Size(28, 22);
            this._infoLabel.Text = "Info";
            // 
            // _pictureBox
            // 
            this._pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pictureBox.Location = new System.Drawing.Point(220, 25);
            this._pictureBox.Name = "_pictureBox";
            this._pictureBox.Size = new System.Drawing.Size(718, 635);
            this._pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._pictureBox.TabIndex = 1;
            this._pictureBox.TabStop = false;
            // 
            // _imageDrawListBox
            // 
            this._imageDrawListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this._imageDrawListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this._imageDrawListBox.FormattingEnabled = true;
            this._imageDrawListBox.ImageList = null;
            this._imageDrawListBox.ItemHeight = 40;
            this._imageDrawListBox.Location = new System.Drawing.Point(0, 25);
            this._imageDrawListBox.Name = "_imageDrawListBox";
            this._imageDrawListBox.Size = new System.Drawing.Size(220, 635);
            this._imageDrawListBox.TabIndex = 2;
            // 
            // _viewCaptureScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._pictureBox);
            this.Controls.Add(this._imageDrawListBox);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_viewCaptureScreen";
            this.Size = new System.Drawing.Size(938, 660);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox _userListComboBox;
        private System.Windows.Forms.ToolStripLabel _infoLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton _prevButton;
        private System.Windows.Forms.ToolStripButton _nextButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _closeButton;
        private System.Windows.Forms.PictureBox _pictureBox;
        private System.Windows.Forms.ToolStripLabel _totalLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private _imageListBox _imageDrawListBox;
    }
}
