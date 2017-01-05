namespace SMLERPControl
{
    partial class _getPictureFullScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_getPictureFullScreen));
            this._pictureBox = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonFull = new MyLib.ToolStripMyButton();
            this._buttonNormal = new MyLib.ToolStripMyButton();
            this._panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this._panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _pictureBox
            // 
            this._pictureBox.Location = new System.Drawing.Point(0, 0);
            this._pictureBox.Name = "_pictureBox";
            this._pictureBox.Size = new System.Drawing.Size(429, 400);
            this._pictureBox.TabIndex = 0;
            this._pictureBox.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::SMLERPControl.Properties.Resources.bt03;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonFull,
            this._buttonNormal});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(539, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _buttonFull
            // 
            this._buttonFull.Image = ((System.Drawing.Image)(resources.GetObject("_buttonFull.Image")));
            this._buttonFull.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonFull.Name = "_buttonFull";
            this._buttonFull.ResourceName = "แสดงภาพเต็มหน้า";
            this._buttonFull.Padding = new System.Windows.Forms.Padding(1);
            this._buttonFull.Size = new System.Drawing.Size(111, 22);
            this._buttonFull.Text = "แสดงภาพเต็มหน้า";
            this._buttonFull.Click += new System.EventHandler(this._buttonFull_Click);
            // 
            // _buttonNormal
            // 
            this._buttonNormal.Image = ((System.Drawing.Image)(resources.GetObject("_buttonNormal.Image")));
            this._buttonNormal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonNormal.Name = "_buttonNormal";
            this._buttonNormal.ResourceName = "แสดงภาพเท่าขนาดจริง";
            this._buttonNormal.Padding = new System.Windows.Forms.Padding(1);
            this._buttonNormal.Size = new System.Drawing.Size(133, 22);
            this._buttonNormal.Text = "แสดงภาพเท่าขนาดจริง";
            this._buttonNormal.Click += new System.EventHandler(this._buttonNormal_Click);
            // 
            // _panel1
            // 
            this._panel1.AutoScroll = true;
            this._panel1.Controls.Add(this._pictureBox);
            this._panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panel1.Location = new System.Drawing.Point(0, 25);
            this._panel1.Name = "_panel1";
            this._panel1.Size = new System.Drawing.Size(539, 456);
            this._panel1.TabIndex = 2;
            // 
            // _getPictureFullScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(539, 481);
            this.Controls.Add(this._panel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_getPictureFullScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Picture";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this._getPictureFullScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this._panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox _pictureBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private MyLib.ToolStripMyButton _buttonFull;
        private MyLib.ToolStripMyButton _buttonNormal;
        private System.Windows.Forms.Panel _panel1;
    }
}