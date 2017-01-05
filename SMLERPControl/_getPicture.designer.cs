namespace SMLERPControl
{
    partial class _getPicture
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_getPicture));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webCamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonGetPicture = new MyLib.ToolStripMyButton();
            this._buttonPaste = new MyLib.ToolStripMyButton();
            this._buttonPictureMode = new MyLib.ToolStripMyButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtWidth = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.txtHeight = new System.Windows.Forms.ToolStripTextBox();
            this._lblSize = new System.Windows.Forms.ToolStripLabel();
            this._txtSize = new System.Windows.Forms.ToolStripTextBox();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this._pictureZoom = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pictureZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolDelete,
            this.pasteToolStripMenuItem,
            this.browsToolStripMenuItem,
            this.webCamToolStripMenuItem,
            this.scannerToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(124, 114);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolDelete
            // 
            this.toolDelete.Image = global::SMLERPControl.Properties.Resources.document_delete;
            this.toolDelete.Name = "toolDelete";
            this.toolDelete.Size = new System.Drawing.Size(123, 22);
            this.toolDelete.Text = "Delete";
            this.toolDelete.Click += new System.EventHandler(this.cccToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // browsToolStripMenuItem
            // 
            this.browsToolStripMenuItem.Name = "browsToolStripMenuItem";
            this.browsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.browsToolStripMenuItem.Text = "Brows";
            this.browsToolStripMenuItem.Click += new System.EventHandler(this.browsToolStripMenuItem_Click);
            // 
            // webCamToolStripMenuItem
            // 
            this.webCamToolStripMenuItem.Name = "webCamToolStripMenuItem";
            this.webCamToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.webCamToolStripMenuItem.Text = "WebCam";
            this.webCamToolStripMenuItem.Click += new System.EventHandler(this.webCamToolStripMenuItem_Click);
            // 
            // scannerToolStripMenuItem
            // 
            this.scannerToolStripMenuItem.Name = "scannerToolStripMenuItem";
            this.scannerToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.scannerToolStripMenuItem.Text = "Scanner";
            this.scannerToolStripMenuItem.Click += new System.EventHandler(this.scannerToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonGetPicture,
            this._buttonPaste,
            this._buttonPictureMode,
            this.toolStripLabel1,
            this.txtWidth,
            this.toolStripLabel2,
            this.txtHeight,
            this._lblSize,
            this._txtSize});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(787, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _buttonGetPicture
            // 
            this._buttonGetPicture.Image = global::SMLERPControl.Properties.Resources.view;
            this._buttonGetPicture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonGetPicture.Name = "_buttonGetPicture";
            this._buttonGetPicture.Padding = new System.Windows.Forms.Padding(1);
            this._buttonGetPicture.ResourceName = "เลือกรูป";
            this._buttonGetPicture.Size = new System.Drawing.Size(63, 22);
            this._buttonGetPicture.Text = "เลือกรูป";
            this._buttonGetPicture.Click += new System.EventHandler(this.toolStripMyButton1_Click);
            // 
            // _buttonPaste
            // 
            this._buttonPaste.Image = global::SMLERPControl.Properties.Resources.branch_element;
            this._buttonPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonPaste.Name = "_buttonPaste";
            this._buttonPaste.Padding = new System.Windows.Forms.Padding(1);
            this._buttonPaste.ResourceName = "วางจาก Clipboard";
            this._buttonPaste.Size = new System.Drawing.Size(117, 22);
            this._buttonPaste.Text = "วางจาก Clipboard";
            this._buttonPaste.Click += new System.EventHandler(this.toolStripMyButton3_Click);
            // 
            // _buttonPictureMode
            // 
            this._buttonPictureMode.Image = ((System.Drawing.Image)(resources.GetObject("_buttonPictureMode.Image")));
            this._buttonPictureMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonPictureMode.Name = "_buttonPictureMode";
            this._buttonPictureMode.Padding = new System.Windows.Forms.Padding(1);
            this._buttonPictureMode.ResourceName = "เปลี่ยนขนาดรูป";
            this._buttonPictureMode.Size = new System.Drawing.Size(96, 22);
            this._buttonPictureMode.Text = "เปลี่ยนขนาดรูป";
            this._buttonPictureMode.Click += new System.EventHandler(this._buttonPictureMode_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(45, 22);
            this.toolStripLabel1.Text = "Width :";
            // 
            // txtWidth
            // 
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(70, 25);
            this.txtWidth.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(49, 22);
            this.toolStripLabel2.Text = "Height :";
            // 
            // txtHeight
            // 
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(70, 25);
            this.txtHeight.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _lblSize
            // 
            this._lblSize.Name = "_lblSize";
            this._lblSize.Size = new System.Drawing.Size(33, 22);
            this._lblSize.Text = "Size :";
            // 
            // _txtSize
            // 
            this._txtSize.Name = "_txtSize";
            this._txtSize.Size = new System.Drawing.Size(70, 25);
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(0, 25);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(787, 0);
            this.flowLayoutPanel4.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this._pictureZoom);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(787, 439);
            this.panel1.TabIndex = 12;
            this.panel1.SizeChanged += new System.EventHandler(this.panel1_SizeChanged);
            // 
            // _pictureZoom
            // 
            this._pictureZoom.BackColor = System.Drawing.Color.Transparent;
            this._pictureZoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pictureZoom.InitialImage = global::SMLERPControl.Properties.Resources.refresh;
            this._pictureZoom.Location = new System.Drawing.Point(6, 6);
            this._pictureZoom.Name = "_pictureZoom";
            this._pictureZoom.Size = new System.Drawing.Size(375, 196);
            this._pictureZoom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._pictureZoom.TabIndex = 4;
            this._pictureZoom.TabStop = false;
            this._pictureZoom.WaitOnLoad = true;
            // 
            // _getPicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel4);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_getPicture";
            this.Size = new System.Drawing.Size(787, 464);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._pictureZoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        /*  private System.Windows.Forms.PictureBox pictureBox1;
          private System.Windows.Forms.PictureBox pictureBox2;
          private System.Windows.Forms.PictureBox pictureBox3;*/
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolDelete;
        private System.Windows.Forms.ToolStripMenuItem browsToolStripMenuItem;
        private MyLib.ToolStripMyButton _buttonGetPicture;
        private MyLib.ToolStripMyButton _buttonPaste;
        public System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtWidth;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox txtHeight;
        private System.Windows.Forms.ToolStripMenuItem webCamToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel _lblSize;
        private System.Windows.Forms.ToolStripTextBox _txtSize;
        private System.Windows.Forms.ToolStripMenuItem scannerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private MyLib.ToolStripMyButton _buttonPictureMode;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.PictureBox _pictureZoom;
    }
}
