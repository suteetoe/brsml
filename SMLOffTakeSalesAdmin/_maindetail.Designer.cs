namespace SMLOffTakeSalesAdmin
{
    partial class _maindetail
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
            this.label1 = new System.Windows.Forms.Label();
            this._dataGridView = new System.Windows.Forms.DataGridView();
            this._textFileTextBox = new System.Windows.Forms.TextBox();
            this._myPanel1 = new MyLib._myPanel();
            this._myFlowLayoutPanel2 = new MyLib._myFlowLayoutPanel();
            this._selectFileButton = new MyLib.VistaButton();
            this._bntPreview = new MyLib.VistaButton();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this.lblProgress = new System.Windows.Forms.Label();
            this._bntClose = new MyLib.VistaButton();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).BeginInit();
            this._myPanel1.SuspendLayout();
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
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Xml File :";
            // 
            // _dataGridView
            // 
            this._dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGridView.Location = new System.Drawing.Point(0, 30);
            this._dataGridView.Name = "_dataGridView";
            this._dataGridView.Size = new System.Drawing.Size(721, 472);
            this._dataGridView.TabIndex = 8;
            // 
            // _textFileTextBox
            // 
            this._textFileTextBox.Location = new System.Drawing.Point(84, 3);
            this._textFileTextBox.Name = "_textFileTextBox";
            this._textFileTextBox.Size = new System.Drawing.Size(257, 20);
            this._textFileTextBox.TabIndex = 1;
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
            this._myPanel1.Size = new System.Drawing.Size(721, 532);
            this._myPanel1.TabIndex = 8;
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
            this._myFlowLayoutPanel2.Size = new System.Drawing.Size(721, 30);
            this._myFlowLayoutPanel2.TabIndex = 7;
            // 
            // _selectFileButton
            // 
            this._selectFileButton.AutoSize = true;
            this._selectFileButton.BackColor = System.Drawing.Color.Transparent;
            this._selectFileButton.ButtonText = "Select Xml File";
            this._selectFileButton.Location = new System.Drawing.Point(347, 3);
            this._selectFileButton.Name = "_selectFileButton";
            this._selectFileButton.Size = new System.Drawing.Size(95, 24);
            this._selectFileButton.TabIndex = 2;
            this._selectFileButton.UseVisualStyleBackColor = true;
            // 
            // _bntPreview
            // 
            this._bntPreview.AutoSize = true;
            this._bntPreview.BackColor = System.Drawing.Color.Transparent;
            this._bntPreview.ButtonText = "Process";
            this._bntPreview.Location = new System.Drawing.Point(448, 3);
            this._bntPreview.Name = "_bntPreview";
            this._bntPreview.Size = new System.Drawing.Size(60, 24);
            this._bntPreview.TabIndex = 5;
            this._bntPreview.UseVisualStyleBackColor = true;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this.lblProgress);
            this._myFlowLayoutPanel1.Controls.Add(this._bntClose);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 502);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(721, 30);
            this._myFlowLayoutPanel1.TabIndex = 6;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(718, 0);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(0, 13);
            this.lblProgress.TabIndex = 8;
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _bntClose
            // 
            this._bntClose.AutoSize = true;
            this._bntClose.BackColor = System.Drawing.Color.Transparent;
            this._bntClose.ButtonText = "Close";
            this._bntClose.Location = new System.Drawing.Point(664, 3);
            this._bntClose.Name = "_bntClose";
            this._bntClose.Size = new System.Drawing.Size(48, 24);
            this._bntClose.TabIndex = 9;
            this._bntClose.UseVisualStyleBackColor = true;
            // 
            // _maindetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel1);
            this.Name = "_maindetail";
            this.Size = new System.Drawing.Size(721, 532);
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).EndInit();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myFlowLayoutPanel2.ResumeLayout(false);
            this._myFlowLayoutPanel2.PerformLayout();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private MyLib._myPanel _myPanel1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel2;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private System.Windows.Forms.Label lblProgress;
        public MyLib.VistaButton _selectFileButton;
        public MyLib.VistaButton _bntPreview;
        public System.Windows.Forms.DataGridView _dataGridView;
        public System.Windows.Forms.TextBox _textFileTextBox;
        public MyLib.VistaButton _bntClose;
    }
}
