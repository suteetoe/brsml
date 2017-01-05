namespace SMLPosClient
{
    partial class _summaryUserControl
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
            this._screen = new MyLib._myScreen();
            this._myPanel1 = new MyLib._myPanel();
            this._flowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this._saveButton = new MyLib.VistaButton();
            this._closeButton = new MyLib.VistaButton();
            this._myPanel1.SuspendLayout();
            this._flowLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // _screen
            // 
            this._screen._isChange = false;
            this._screen.BackColor = System.Drawing.Color.Transparent;
            this._screen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._screen.Location = new System.Drawing.Point(10, 10);
            this._screen.Name = "_screen";
            this._screen.Size = new System.Drawing.Size(696, 434);
            this._screen.TabIndex = 0;
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
            this._myPanel1.Size = new System.Drawing.Size(716, 454);
            this._myPanel1.TabIndex = 1;
            // 
            // _flowLayout
            // 
            this._flowLayout.AutoSize = true;
            this._flowLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._flowLayout.Controls.Add(this._saveButton);
            this._flowLayout.Controls.Add(this._closeButton);
            this._flowLayout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._flowLayout.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._flowLayout.Location = new System.Drawing.Point(0, 454);
            this._flowLayout.Name = "_flowLayout";
            this._flowLayout.Size = new System.Drawing.Size(716, 30);
            this._flowLayout.TabIndex = 1;
            // 
            // _saveButton
            // 
            this._saveButton.AutoSize = true;
            this._saveButton.BackColor = System.Drawing.Color.Transparent;
            this._saveButton.ButtonText = "บันทึก";
            this._saveButton.Location = new System.Drawing.Point(651, 3);
            this._saveButton.myImage = global::SMLPosClient.Properties.Resources.disk_blue1;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(62, 24);
            this._saveButton.TabIndex = 0;
            this._saveButton.Text = "บันทึก";
            this._saveButton.UseVisualStyleBackColor = false;
            // 
            // _closeButton
            // 
            this._closeButton.AutoSize = true;
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.ButtonText = "ปิด";
            this._closeButton.Location = new System.Drawing.Point(597, 3);
            this._closeButton.myImage = global::SMLPosClient.Properties.Resources.error1;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(48, 24);
            this._closeButton.TabIndex = 1;
            this._closeButton.Text = "ปิด";
            this._closeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._closeButton.UseVisualStyleBackColor = false;
            // 
            // _summaryUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this._flowLayout);
            this.Name = "_summaryUserControl";
            this.Size = new System.Drawing.Size(716, 484);
            this._myPanel1.ResumeLayout(false);
            this._flowLayout.ResumeLayout(false);
            this._flowLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myScreen _screen;
        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.FlowLayoutPanel _flowLayout;
        public MyLib.VistaButton _saveButton;
        public MyLib.VistaButton _closeButton;
    }
}
