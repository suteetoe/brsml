namespace SMLERPConfig
{
    partial class _provinceLoadFromWebControl
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
            this._myGrid1 = new MyLib._myGrid();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this.vistaButton2 = new MyLib.VistaButton();
            this._processButton = new MyLib.VistaButton();
            this._selectAllButton = new MyLib.VistaButton();
            this._myPanel1 = new MyLib._myPanel();
            this._statusLabel = new MyLib._myLabel();
            this._timer1 = new System.Windows.Forms.Timer(this.components);
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myGrid1
            // 
            this._myGrid1.BackColor = System.Drawing.SystemColors.Window;
            this._myGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myGrid1.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._myGrid1.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._myGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myGrid1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myGrid1.IsEdit = false;
            this._myGrid1.Location = new System.Drawing.Point(0, 0);
            this._myGrid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._myGrid1.Name = "_myGrid1";
            this._myGrid1.Size = new System.Drawing.Size(755, 445);
            this._myGrid1.TabIndex = 0;
            this._myGrid1.TabStop = false;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this.vistaButton2);
            this._myFlowLayoutPanel1.Controls.Add(this._processButton);
            this._myFlowLayoutPanel1.Controls.Add(this._selectAllButton);
            this._myFlowLayoutPanel1.Controls.Add(this._progressBar);
            this._myFlowLayoutPanel1.Controls.Add(this._statusLabel);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 445);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(755, 30);
            this._myFlowLayoutPanel1.TabIndex = 1;
            // 
            // vistaButton2
            // 
            this.vistaButton2.AutoSize = true;
            this.vistaButton2.BackColor = System.Drawing.Color.Transparent;
            this.vistaButton2.ButtonText = "Close";
            this.vistaButton2.Location = new System.Drawing.Point(690, 3);
            this.vistaButton2.myImage = global::SMLERPConfig.Resource16x16.error;
            this.vistaButton2.Name = "vistaButton2";
            this.vistaButton2.Size = new System.Drawing.Size(62, 24);
            this.vistaButton2.TabIndex = 1;
            this.vistaButton2.UseVisualStyleBackColor = true;
            this.vistaButton2.Click += new System.EventHandler(this.vistaButton2_Click);
            // 
            // _processButton
            // 
            this._processButton.AutoSize = true;
            this._processButton.BackColor = System.Drawing.Color.Transparent;
            this._processButton.ButtonText = "Process";
            this._processButton.Location = new System.Drawing.Point(611, 3);
            this._processButton.myImage = global::SMLERPConfig.Resource16x16.flash;
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(73, 24);
            this._processButton.TabIndex = 0;
            this._processButton.UseVisualStyleBackColor = true;
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _selectAllButton
            // 
            this._selectAllButton.AutoSize = true;
            this._selectAllButton.BackColor = System.Drawing.Color.Transparent;
            this._selectAllButton.ButtonText = "Select All";
            this._selectAllButton.Location = new System.Drawing.Point(524, 3);
            this._selectAllButton.myImage = global::SMLERPConfig.Resource16x16.lightbulb_on;
            this._selectAllButton.Name = "_selectAllButton";
            this._selectAllButton.Size = new System.Drawing.Size(81, 24);
            this._selectAllButton.TabIndex = 2;
            this._selectAllButton.UseVisualStyleBackColor = true;
            this._selectAllButton.Click += new System.EventHandler(this._selectAllButton_Click);
            // 
            // _myPanel1
            // 
            this._myPanel1.AutoSize = true;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 445);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(755, 0);
            this._myPanel1.TabIndex = 2;
            // 
            // _statusLabel
            // 
            this._statusLabel.AutoSize = true;
            this._statusLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._statusLabel.Location = new System.Drawing.Point(122, 0);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.ResourceName = "";
            this._statusLabel.Size = new System.Drawing.Size(0, 19);
            this._statusLabel.TabIndex = 0;
            this._statusLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // _timer1
            // 
            this._timer1.Tick += new System.EventHandler(this._timer1_Tick);
            // 
            // _progressBar
            // 
            this._progressBar.Location = new System.Drawing.Point(128, 3);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(390, 23);
            this._progressBar.TabIndex = 3;
            // 
            // _provinceLoadFromWebrControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myGrid1);
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this._myFlowLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_provinceLoadFromWebrControl";
            this.Size = new System.Drawing.Size(755, 475);
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myGrid _myGrid1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib.VistaButton vistaButton2;
        private MyLib.VistaButton _processButton;
        private MyLib.VistaButton _selectAllButton;
        private MyLib._myPanel _myPanel1;
        private MyLib._myLabel _statusLabel;
        private System.Windows.Forms.Timer _timer1;
        private System.Windows.Forms.ProgressBar _progressBar;
    }
}
