namespace SMLPOSControl
{
    partial class _posDownloadSound
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
            this._downloadProgressbar = new System.Windows.Forms.ProgressBar();
            this._resultLabel = new System.Windows.Forms.Label();
            this._myPanel1 = new MyLib._myPanel();
            this._startButton = new MyLib.VistaButton();
            this._stopButton = new MyLib.VistaButton();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _downloadProgressbar
            // 
            this._downloadProgressbar.Location = new System.Drawing.Point(15, 15);
            this._downloadProgressbar.Name = "_downloadProgressbar";
            this._downloadProgressbar.Size = new System.Drawing.Size(348, 23);
            this._downloadProgressbar.TabIndex = 0;
            // 
            // _resultLabel
            // 
            this._resultLabel.BackColor = System.Drawing.Color.Transparent;
            this._resultLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._resultLabel.Location = new System.Drawing.Point(12, 41);
            this._resultLabel.Name = "_resultLabel";
            this._resultLabel.Size = new System.Drawing.Size(351, 34);
            this._resultLabel.TabIndex = 1;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._startButton);
            this._myPanel1.Controls.Add(this._stopButton);
            this._myPanel1.Controls.Add(this._downloadProgressbar);
            this._myPanel1.Controls.Add(this._resultLabel);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(375, 114);
            this._myPanel1.TabIndex = 2;
            // 
            // _startButton
            // 
            this._startButton.BackColor = System.Drawing.Color.Transparent;
            this._startButton.ButtonText = "Start";
            this._startButton.Location = new System.Drawing.Point(284, 81);
            this._startButton.Name = "_startButton";
            this._startButton.Size = new System.Drawing.Size(80, 23);
            this._startButton.TabIndex = 3;
            this._startButton.Text = "vistaButton2";
            this._startButton.UseVisualStyleBackColor = false;
            this._startButton.Click += new System.EventHandler(this._startButton_Click);
            // 
            // _stopButton
            // 
            this._stopButton.BackColor = System.Drawing.Color.Transparent;
            this._stopButton.ButtonText = "Stop";
            this._stopButton.Location = new System.Drawing.Point(195, 81);
            this._stopButton.Name = "_stopButton";
            this._stopButton.Size = new System.Drawing.Size(80, 23);
            this._stopButton.TabIndex = 2;
            this._stopButton.Text = "vistaButton1";
            this._stopButton.UseVisualStyleBackColor = false;
            this._stopButton.Click += new System.EventHandler(this._stopButton_Click);
            // 
            // _posDownloadSound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 114);
            this.Controls.Add(this._myPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_posDownloadSound";
            this.Text = " Download And Install POS Sound";
            this._myPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar _downloadProgressbar;
        private System.Windows.Forms.Label _resultLabel;
        private MyLib._myPanel _myPanel1;
        private MyLib.VistaButton _startButton;
        private MyLib.VistaButton _stopButton;
    }
}