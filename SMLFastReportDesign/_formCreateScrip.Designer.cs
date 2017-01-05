namespace SMLFastReportDesign
{
    partial class _formCreateScrip
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_formCreateScrip));
            this._textScrip = new System.Windows.Forms.TextBox();
            this._buttonSave = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _textScrip
            // 
            this._textScrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this._textScrip.Location = new System.Drawing.Point(0, 0);
            this._textScrip.Multiline = true;
            this._textScrip.Name = "_textScrip";
            this._textScrip.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._textScrip.Size = new System.Drawing.Size(531, 519);
            this._textScrip.TabIndex = 0;
            // 
            // _buttonSave
            // 
            this._buttonSave.Enabled = false;
            this._buttonSave.Location = new System.Drawing.Point(453, 3);
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Size = new System.Drawing.Size(75, 23);
            this._buttonSave.TabIndex = 0;
            this._buttonSave.Text = "Save";
            this._buttonSave.UseVisualStyleBackColor = true;
            this._buttonSave.Click += new System.EventHandler(this._buttonSave_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this._buttonSave);
            this.flowLayoutPanel1.Controls.Add(this.progressBar1);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 519);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(531, 30);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(143, 3);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(304, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // _timer
            // 
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(137, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(0, 25);
            this.label1.TabIndex = 2;
            // 
            // _formCreateScrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 549);
            this.Controls.Add(this._textScrip);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "_formCreateScrip";
            this.Text = "Generate Scrip";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _textScrip;
        private System.Windows.Forms.Button _buttonSave;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer _timer;
        private System.Windows.Forms.Label label1;
    }
}