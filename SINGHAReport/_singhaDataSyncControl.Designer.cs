namespace SINGHAReport
{
    partial class _singhaDataSyncControl
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
            this._myScreen1 = new MyLib._myScreen();
            this._myPanel1 = new MyLib._myPanel();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._syncButton = new MyLib.VistaButton();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this._closeButton = new MyLib.VistaButton();
            this._myPanel1.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myScreen1
            // 
            this._myScreen1._isChange = false;
            this._myScreen1.BackColor = System.Drawing.Color.Transparent;
            this._myScreen1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myScreen1.Location = new System.Drawing.Point(0, 0);
            this._myScreen1.Name = "_myScreen1";
            this._myScreen1.Size = new System.Drawing.Size(856, 10);
            this._myScreen1.TabIndex = 0;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.Controls.Add(this._myScreen1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(856, 572);
            this._myPanel1.TabIndex = 1;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._syncButton);
            this._myFlowLayoutPanel1.Controls.Add(this.progressBar1);
            this._myFlowLayoutPanel1.Controls.Add(this._closeButton);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 10);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(856, 34);
            this._myFlowLayoutPanel1.TabIndex = 1;
            // 
            // _syncButton
            // 
            this._syncButton._drawNewMethod = false;
            this._syncButton.BackColor = System.Drawing.Color.Transparent;
            this._syncButton.ButtonText = "Send Data";
            this._syncButton.Location = new System.Drawing.Point(3, 3);
            this._syncButton.myImage = global::SINGHAReport.Properties.Resources.flash;
            this._syncButton.Name = "_syncButton";
            this._syncButton.Size = new System.Drawing.Size(100, 25);
            this._syncButton.TabIndex = 0;
            this._syncButton.Text = "vistaButton1";
            this._syncButton.UseVisualStyleBackColor = false;
            this._syncButton.Click += new System.EventHandler(this._syncButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(109, 3);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(461, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // _closeButton
            // 
            this._closeButton._drawNewMethod = false;
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.ButtonText = "Close";
            this._closeButton.Location = new System.Drawing.Point(576, 3);
            this._closeButton.myImage = global::SINGHAReport.Properties.Resources.error;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(85, 25);
            this._closeButton.TabIndex = 2;
            this._closeButton.Text = "vistaButton1";
            this._closeButton.UseVisualStyleBackColor = false;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _singhaDataSyncControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel1);
            this.Name = "_singhaDataSyncControl";
            this.Size = new System.Drawing.Size(856, 572);
            this._myPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myScreen _myScreen1;
        private MyLib._myPanel _myPanel1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib.VistaButton _syncButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private MyLib.VistaButton _closeButton;
    }
}
