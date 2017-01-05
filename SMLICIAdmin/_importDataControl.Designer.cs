namespace SMLICIAdmin
{
    partial class _importICIDataControl
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
            this.label2 = new System.Windows.Forms.Label();
            this._tableComboBox = new System.Windows.Forms.ComboBox();
            this._selectFileButton = new MyLib.VistaButton();
            this._myPanel1 = new MyLib._myPanel();
            this._mapGrid = new MyLib._myGrid();
            this._myFlowLayoutPanel2 = new MyLib._myFlowLayoutPanel();
            this._mapFieldButton = new MyLib.VistaButton();
            this._bntPreview = new MyLib.VistaButton();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._closeButton = new MyLib.VistaButton();
            this._processButton = new MyLib.VistaButton();
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
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(463, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "To Table :";
            // 
            // _tableComboBox
            // 
            this._tableComboBox.FormattingEnabled = true;
            this._tableComboBox.Location = new System.Drawing.Point(533, 3);
            this._tableComboBox.Name = "_tableComboBox";
            this._tableComboBox.Size = new System.Drawing.Size(246, 22);
            this._tableComboBox.TabIndex = 3;
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
            this._myPanel1.Controls.Add(this._mapGrid);
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
            // _mapGrid
            // 
            this._mapGrid._extraWordShow = true;
            this._mapGrid.BackColor = System.Drawing.SystemColors.Window;
            this._mapGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._mapGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._mapGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mapGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._mapGrid.Location = new System.Drawing.Point(0, 60);
            this._mapGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._mapGrid.Name = "_mapGrid";
            this._mapGrid.Size = new System.Drawing.Size(904, 521);
            this._mapGrid.TabIndex = 8;
            this._mapGrid.TabStop = false;
            // 
            // _myFlowLayoutPanel2
            // 
            this._myFlowLayoutPanel2.AutoSize = true;
            this._myFlowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel2.Controls.Add(this.label1);
            this._myFlowLayoutPanel2.Controls.Add(this._textFileTextBox);
            this._myFlowLayoutPanel2.Controls.Add(this._selectFileButton);
            this._myFlowLayoutPanel2.Controls.Add(this.label2);
            this._myFlowLayoutPanel2.Controls.Add(this._tableComboBox);
            this._myFlowLayoutPanel2.Controls.Add(this._mapFieldButton);
            this._myFlowLayoutPanel2.Controls.Add(this._bntPreview);
            this._myFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this._myFlowLayoutPanel2.Name = "_myFlowLayoutPanel2";
            this._myFlowLayoutPanel2.Size = new System.Drawing.Size(904, 60);
            this._myFlowLayoutPanel2.TabIndex = 7;
            // 
            // _mapFieldButton
            // 
            this._mapFieldButton.AutoSize = true;
            this._mapFieldButton.BackColor = System.Drawing.Color.Transparent;
            this._mapFieldButton.ButtonText = "Map Field";
            this._mapFieldButton.Location = new System.Drawing.Point(785, 3);
            this._mapFieldButton.Name = "_mapFieldButton";
            this._mapFieldButton.Size = new System.Drawing.Size(72, 24);
            this._mapFieldButton.TabIndex = 4;
            this._mapFieldButton.UseVisualStyleBackColor = true;
            this._mapFieldButton.Click += new System.EventHandler(this._mapFieldButton_Click);
            // 
            // _bntPreview
            // 
            this._bntPreview.AutoSize = true;
            this._bntPreview.BackColor = System.Drawing.Color.Transparent;
            this._bntPreview.ButtonText = "Preview";
            this._bntPreview.Location = new System.Drawing.Point(3, 33);
            this._bntPreview.Name = "_bntPreview";
            this._bntPreview.Size = new System.Drawing.Size(62, 24);
            this._bntPreview.TabIndex = 5;
            this._bntPreview.UseVisualStyleBackColor = true;
            this._bntPreview.Click += new System.EventHandler(this._bntPreview_Click);
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._closeButton);
            this._myFlowLayoutPanel1.Controls.Add(this._processButton);
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
            // _processButton
            // 
            this._processButton.AutoSize = true;
            this._processButton.BackColor = System.Drawing.Color.Transparent;
            this._processButton.ButtonText = "Process";
            this._processButton.Location = new System.Drawing.Point(786, 3);
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(61, 24);
            this._processButton.TabIndex = 6;
            this._processButton.UseVisualStyleBackColor = true;
            this._processButton.Click += new System.EventHandler(this.vistaButton2_Click);
            // 
            // _importICIDataControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_importICIDataControl";
            this.Size = new System.Drawing.Size(904, 611);
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
        private System.Windows.Forms.TextBox _textFileTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _tableComboBox;
        private MyLib.VistaButton _selectFileButton;
        private MyLib._myPanel _myPanel1;
        private MyLib._myGrid _mapGrid;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel2;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib.VistaButton _closeButton;
        private MyLib.VistaButton _processButton;
        private MyLib.VistaButton _mapFieldButton;
        private MyLib.VistaButton _bntPreview;
    }
}
