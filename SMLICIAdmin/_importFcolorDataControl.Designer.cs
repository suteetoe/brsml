namespace SMLICIAdmin
{
    partial class _importFcolorDataControl
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
            this.label1 = new System.Windows.Forms.Label();
            this._textFileTextBox = new System.Windows.Forms.TextBox();
            this._selectFileButton = new MyLib.VistaButton();
            this._myPanel1 = new MyLib._myPanel();
            this._dataGridView = new System.Windows.Forms.DataGridView();
            this._myFlowLayoutPanel2 = new MyLib._myFlowLayoutPanel();
            this._bntPreview = new MyLib.VistaButton();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._closeButton = new MyLib.VistaButton();
            this._bntViewProcess = new MyLib.VistaButton();
            this._processButton = new MyLib.VistaButton();
            this.@__progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this._myPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).BeginInit();
            this._myFlowLayoutPanel2.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Xml File :";
            // 
            // _textFileTextBox
            // 
            this._textFileTextBox.Location = new System.Drawing.Point(95, 3);
            this._textFileTextBox.Name = "_textFileTextBox";
            this._textFileTextBox.Size = new System.Drawing.Size(257, 22);
            this._textFileTextBox.TabIndex = 1;
            // 
            // _selectFileButton
            // 
            this._selectFileButton.AutoSize = true;
            this._selectFileButton.BackColor = System.Drawing.Color.Transparent;
            this._selectFileButton.ButtonText = "Select Xml File";
            this._selectFileButton.Location = new System.Drawing.Point(358, 3);
            this._selectFileButton.Name = "_selectFileButton";
            this._selectFileButton.Size = new System.Drawing.Size(99, 24);
            this._selectFileButton.TabIndex = 2;
            this._selectFileButton.UseVisualStyleBackColor = true;
            this._selectFileButton.Click += new System.EventHandler(this._selectFileButton1_Click);
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._dataGridView);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel2);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(904, 611);
            this._myPanel1.TabIndex = 6;
            // 
            // _dataGridView
            // 
            this._dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGridView.Location = new System.Drawing.Point(0, 30);
            this._dataGridView.Name = "_dataGridView";
            this._dataGridView.Size = new System.Drawing.Size(904, 551);
            this._dataGridView.TabIndex = 8;
            // 
            // _myFlowLayoutPanel2
            // 
            this._myFlowLayoutPanel2.AutoSize = true;
            this._myFlowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel2.Controls.Add(this.label1);
            this._myFlowLayoutPanel2.Controls.Add(this._textFileTextBox);
            this._myFlowLayoutPanel2.Controls.Add(this._selectFileButton);
            this._myFlowLayoutPanel2.Controls.Add(this._bntPreview);
            this._myFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this._myFlowLayoutPanel2.Name = "_myFlowLayoutPanel2";
            this._myFlowLayoutPanel2.Size = new System.Drawing.Size(904, 30);
            this._myFlowLayoutPanel2.TabIndex = 7;
            // 
            // _bntPreview
            // 
            this._bntPreview.AutoSize = true;
            this._bntPreview.BackColor = System.Drawing.Color.Transparent;
            this._bntPreview.ButtonText = "Preview";
            this._bntPreview.Location = new System.Drawing.Point(463, 3);
            this._bntPreview.Name = "_bntPreview";
            this._bntPreview.Size = new System.Drawing.Size(62, 24);
            this._bntPreview.TabIndex = 5;
            this._bntPreview.UseVisualStyleBackColor = true;
            this._bntPreview.Visible = false;
            this._bntPreview.Click += new System.EventHandler(this._bntPreview_Click);
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._closeButton);
            this._myFlowLayoutPanel1.Controls.Add(this._bntViewProcess);
            this._myFlowLayoutPanel1.Controls.Add(this._processButton);
            this._myFlowLayoutPanel1.Controls.Add(this.@__progressBar);
            this._myFlowLayoutPanel1.Controls.Add(this.lblProgress);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 581);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(904, 30);
            this._myFlowLayoutPanel1.TabIndex = 6;
            // 
            // _closeButton
            // 
            this._closeButton.AutoSize = true;
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.ButtonText = "Close";
            this._closeButton.Location = new System.Drawing.Point(853, 3);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(48, 24);
            this._closeButton.TabIndex = 5;
            this._closeButton.UseVisualStyleBackColor = true;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _bntViewProcess
            // 
            this._bntViewProcess.AutoSize = true;
            this._bntViewProcess.BackColor = System.Drawing.Color.Transparent;
            this._bntViewProcess.ButtonText = "ViewProcess";
            this._bntViewProcess.Location = new System.Drawing.Point(760, 3);
            this._bntViewProcess.Name = "_bntViewProcess";
            this._bntViewProcess.Size = new System.Drawing.Size(87, 24);
            this._bntViewProcess.TabIndex = 9;
            this._bntViewProcess.UseVisualStyleBackColor = true;
            this._bntViewProcess.Click += new System.EventHandler(this._bntViewProcess_Click);
            // 
            // _processButton
            // 
            this._processButton.AutoSize = true;
            this._processButton.BackColor = System.Drawing.Color.Transparent;
            this._processButton.ButtonText = "Process";
            this._processButton.Location = new System.Drawing.Point(693, 3);
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(61, 24);
            this._processButton.TabIndex = 6;
            this._processButton.UseVisualStyleBackColor = true;
            this._processButton.Click += new System.EventHandler(this.vistaButton2_Click);
            // 
            // __progressBar
            // 
            this.@__progressBar.Location = new System.Drawing.Point(587, 3);
            this.@__progressBar.Name = "__progressBar";
            this.@__progressBar.Size = new System.Drawing.Size(100, 23);
            this.@__progressBar.TabIndex = 7;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(581, 0);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(0, 14);
            this.lblProgress.TabIndex = 8;
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _timer
            // 
            this._timer.Enabled = true;
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // _importFcolorDataControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_importFcolorDataControl";
            this.Size = new System.Drawing.Size(904, 611);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).EndInit();
            this._myFlowLayoutPanel2.ResumeLayout(false);
            this._myFlowLayoutPanel2.PerformLayout();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _textFileTextBox;
        private MyLib.VistaButton _selectFileButton;
        private MyLib._myPanel _myPanel1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel2;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib.VistaButton _closeButton;
        private MyLib.VistaButton _processButton;
        private MyLib.VistaButton _bntPreview;
        private System.Windows.Forms.DataGridView _dataGridView;
        private System.Windows.Forms.ProgressBar __progressBar;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Timer _timer;
        private MyLib.VistaButton _bntViewProcess;
    }
}
