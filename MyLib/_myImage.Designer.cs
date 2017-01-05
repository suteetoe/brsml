namespace MyLib
{
    partial class _myImage
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
            this._pictureBox = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._selectPictureButton = new MyLib.ToolStripMyButton();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _pictureBox
            // 
            this._pictureBox.BackColor = System.Drawing.Color.White;
            this._pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pictureBox.Location = new System.Drawing.Point(0, 0);
            this._pictureBox.Name = "_pictureBox";
            this._pictureBox.Size = new System.Drawing.Size(387, 227);
            this._pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._pictureBox.TabIndex = 0;
            this._pictureBox.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Menu;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._selectPictureButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 227);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(387, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _selectPictureButton
            // 
            this._selectPictureButton.Image = global::MyLib.Properties.Resources.filesave;
            this._selectPictureButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._selectPictureButton.Name = "_selectPictureButton";
            this._selectPictureButton.Padding = new System.Windows.Forms.Padding(1);
            this._selectPictureButton.ResourceName = "";
            this._selectPictureButton.Size = new System.Drawing.Size(100, 22);
            this._selectPictureButton.Text = "Select Picture";
            this._selectPictureButton.Click += new System.EventHandler(this._selectPictureButton_Click);
            // 
            // _myImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this._pictureBox);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_myImage";
            this.Size = new System.Drawing.Size(387, 252);
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox _pictureBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private ToolStripMyButton _selectPictureButton;
    }
}
