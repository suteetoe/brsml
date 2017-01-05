namespace SMLPosClient
{
    partial class _summaryDayUserControl
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._closeButton = new MyLib.VistaButton();
            this._previewButton = new MyLib.VistaButton();
            this._printButton = new MyLib.VistaButton();
            this._myPanel1 = new MyLib._myPanel();
            this._screen = new MyLib._myScreen();
            this.flowLayoutPanel1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.flowLayoutPanel1.Controls.Add(this._closeButton);
            this.flowLayoutPanel1.Controls.Add(this._previewButton);
            this.flowLayoutPanel1.Controls.Add(this._printButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 318);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(400, 32);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // _closeButton
            // 
            this._closeButton.AutoSize = true;
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.ButtonText = "ปิดจอ";
            this._closeButton.Location = new System.Drawing.Point(337, 3);
            this._closeButton.myImage = global::SMLPosClient.Properties.Resources.error1;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(60, 24);
            this._closeButton.TabIndex = 0;
            this._closeButton.Text = "ปิดจอ";
            this._closeButton.UseVisualStyleBackColor = false;
            // 
            // _previewButton
            // 
            this._previewButton.AutoSize = true;
            this._previewButton.BackColor = System.Drawing.Color.Transparent;
            this._previewButton.ButtonText = "แสดงก่อนพิมพ์";
            this._previewButton.Location = new System.Drawing.Point(232, 3);
            this._previewButton.myImage = global::SMLPosClient.Properties.Resources.printer_view;
            this._previewButton.Name = "_previewButton";
            this._previewButton.Size = new System.Drawing.Size(99, 24);
            this._previewButton.TabIndex = 1;
            this._previewButton.Text = "แสดงก่อนพิมพ์";
            this._previewButton.UseVisualStyleBackColor = false;
            this._previewButton.Click += new System.EventHandler(this._previewButton_Click);
            // 
            // _printButton
            // 
            this._printButton.AutoSize = true;
            this._printButton.BackColor = System.Drawing.Color.Transparent;
            this._printButton.ButtonText = "พิมพ์";
            this._printButton.Location = new System.Drawing.Point(169, 3);
            this._printButton.myImage = global::SMLPosClient.Properties.Resources.printer;
            this._printButton.Name = "_printButton";
            this._printButton.Size = new System.Drawing.Size(57, 24);
            this._printButton.TabIndex = 2;
            this._printButton.Text = "พิมพ์";
            this._printButton.UseVisualStyleBackColor = false;
            this._printButton.Click += new System.EventHandler(this._printButton_Click);
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._screen);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(10);
            this._myPanel1.Size = new System.Drawing.Size(400, 318);
            this._myPanel1.TabIndex = 1;
            // 
            // _screen
            // 
            this._screen._isChange = false;
            this._screen.BackColor = System.Drawing.Color.Transparent;
            this._screen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._screen.Location = new System.Drawing.Point(10, 10);
            this._screen.Name = "_screen";
            this._screen.Size = new System.Drawing.Size(380, 298);
            this._screen.TabIndex = 0;
            // 
            // _summaryDayUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "_summaryDayUserControl";
            this.Size = new System.Drawing.Size(400, 350);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MyLib._myPanel _myPanel1;
        private MyLib._myScreen _screen;
        public MyLib.VistaButton _closeButton;
        public MyLib.VistaButton _previewButton;
        public MyLib.VistaButton _printButton;
    }
}
