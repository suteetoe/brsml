namespace SMLFastReport
{
    partial class _conditionForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._processButton = new MyLib.VistaButton();
            this._closeButton = new MyLib.VistaButton();
            this._myPanel1 = new MyLib._myPanel();
            this._conditionPanel = new MyLib._myPanel();
            this._myFlowLayoutPanel1 = new MyLib._myPanel();
            this._reportNameLabel = new MyLib._myShadowLabel(this.components);
            this.flowLayoutPanel1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this._processButton);
            this.flowLayoutPanel1.Controls.Add(this._closeButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 443);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(783, 32);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // _processButton
            // 
            this._processButton._drawNewMethod = false;
            this._processButton.BackColor = System.Drawing.Color.Transparent;
            this._processButton.ButtonText = "Process";
            this._processButton.Location = new System.Drawing.Point(692, 4);
            this._processButton.myImage = global::SMLFastReport.Properties.Resources.flash;
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(86, 26);
            this._processButton.TabIndex = 0;
            this._processButton.Text = "vistaButton1";
            this._processButton.UseVisualStyleBackColor = false;
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton._drawNewMethod = false;
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.ButtonText = "Close";
            this._closeButton.Location = new System.Drawing.Point(600, 4);
            this._closeButton.myImage = global::SMLFastReport.Properties.Resources.error;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(86, 26);
            this._closeButton.TabIndex = 1;
            this._closeButton.Text = "vistaButton2";
            this._closeButton.UseVisualStyleBackColor = false;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._conditionPanel);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.Controls.Add(this.flowLayoutPanel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(783, 475);
            this._myPanel1.TabIndex = 1;
            // 
            // _conditionPanel
            // 
            this._conditionPanel._switchTabAuto = false;
            this._conditionPanel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._conditionPanel.CornerPicture = null;
            this._conditionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._conditionPanel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._conditionPanel.Location = new System.Drawing.Point(0, 39);
            this._conditionPanel.Name = "_conditionPanel";
            this._conditionPanel.Size = new System.Drawing.Size(783, 404);
            this._conditionPanel.TabIndex = 3;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1._switchTabAuto = false;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myFlowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myFlowLayoutPanel1.Controls.Add(this._reportNameLabel);
            this._myFlowLayoutPanel1.CornerPicture = null;
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(783, 39);
            this._myFlowLayoutPanel1.TabIndex = 2;
            // 
            // _reportNameLabel
            // 
            this._reportNameLabel.Angle = 0F;
            this._reportNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._reportNameLabel.EndColor = System.Drawing.Color.Transparent;
            this._reportNameLabel.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._reportNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this._reportNameLabel.Location = new System.Drawing.Point(5, 5);
            this._reportNameLabel.Name = "_reportNameLabel";
            this._reportNameLabel.ShadowColor = System.Drawing.Color.Gray;
            this._reportNameLabel.Size = new System.Drawing.Size(771, 27);
            this._reportNameLabel.StartColor = System.Drawing.Color.Transparent;
            this._reportNameLabel.TabIndex = 0;
            this._reportNameLabel.Text = "Label";
            this._reportNameLabel.XOffset = 1F;
            this._reportNameLabel.YOffset = 1F;
            // 
            // _conditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 475);
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_conditionForm";
            this.Text = "เงือนไขการแสดงรายงาน";
            this.flowLayoutPanel1.ResumeLayout(false);
            this._myPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MyLib.VistaButton _processButton;
        private MyLib.VistaButton _closeButton;
        private MyLib._myPanel _myFlowLayoutPanel1;
        public MyLib._myShadowLabel _reportNameLabel;
        public MyLib._myPanel _myPanel1;
        public MyLib._myPanel _conditionPanel;
    }
}