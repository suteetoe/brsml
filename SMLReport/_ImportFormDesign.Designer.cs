namespace SMLReport
{
    partial class _ImportFormDesign
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
            this._myPanel1 = new MyLib._myPanel();
            this._dataGrid = new MyLib._myGrid();
            this._myFlowLayoutPanel2 = new MyLib._myFlowLayoutPanel();
            this._buttonClose = new MyLib.VistaButton();
            this._buttonProcess = new MyLib.VistaButton();
            this._selectAllButton = new MyLib.VistaButton();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._myShadowLabel1 = new MyLib._myShadowLabel(this.components);
            this._progressbar = new System.Windows.Forms.ProgressBar();
            this._statusLabel = new MyLib._myLabel();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this._myPanel1.SuspendLayout();
            this._myFlowLayoutPanel2.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._dataGrid);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel2);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(732, 457);
            this._myPanel1.TabIndex = 0;
            // 
            // _dataGrid
            // 
            this._dataGrid.BackColor = System.Drawing.SystemColors.Window;
            this._dataGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._dataGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._dataGrid.IsEdit = false;
            this._dataGrid.Location = new System.Drawing.Point(0, 43);
            this._dataGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._dataGrid.Name = "_dataGrid";
            this._dataGrid.Size = new System.Drawing.Size(732, 382);
            this._dataGrid.TabIndex = 2;
            this._dataGrid.TabStop = false;
            // 
            // _myFlowLayoutPanel2
            // 
            this._myFlowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel2.Controls.Add(this._buttonClose);
            this._myFlowLayoutPanel2.Controls.Add(this._buttonProcess);
            this._myFlowLayoutPanel2.Controls.Add(this._selectAllButton);
            this._myFlowLayoutPanel2.Controls.Add(this._progressbar);
            this._myFlowLayoutPanel2.Controls.Add(this._statusLabel);
            this._myFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel2.Location = new System.Drawing.Point(0, 425);
            this._myFlowLayoutPanel2.Name = "_myFlowLayoutPanel2";
            this._myFlowLayoutPanel2.Size = new System.Drawing.Size(732, 32);
            this._myFlowLayoutPanel2.TabIndex = 1;
            // 
            // _buttonClose
            // 
            this._buttonClose.BackColor = System.Drawing.Color.Transparent;
            this._buttonClose.ButtonText = "Close";
            this._buttonClose.Image = global::SMLReport.Properties.Resources.error;
            this._buttonClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._buttonClose.Location = new System.Drawing.Point(654, 3);
            this._buttonClose.myImage = global::SMLReport.Properties.Resources.error;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Size = new System.Drawing.Size(75, 25);
            this._buttonClose.TabIndex = 10;
            this._buttonClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonClose.UseVisualStyleBackColor = true;
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
            // 
            // _buttonProcess
            // 
            this._buttonProcess.BackColor = System.Drawing.Color.Transparent;
            this._buttonProcess.ButtonText = "Process";
            this._buttonProcess.Image = global::SMLReport.Properties.Resources.flash;
            this._buttonProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._buttonProcess.Location = new System.Drawing.Point(573, 3);
            this._buttonProcess.myImage = global::SMLReport.Properties.Resources.flash;
            this._buttonProcess.Name = "_buttonProcess";
            this._buttonProcess.Size = new System.Drawing.Size(75, 25);
            this._buttonProcess.TabIndex = 11;
            this._buttonProcess.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonProcess.UseVisualStyleBackColor = true;
            this._buttonProcess.Click += new System.EventHandler(this._buttonProcess_Click);
            // 
            // _selectAllButton
            // 
            this._selectAllButton.BackColor = System.Drawing.Color.Transparent;
            this._selectAllButton.ButtonText = "Select All";
            this._selectAllButton.Image = global::SMLReport.Properties.Resources.lightbulb_on;
            this._selectAllButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._selectAllButton.Location = new System.Drawing.Point(492, 3);
            this._selectAllButton.myImage = global::SMLReport.Properties.Resources.lightbulb_on;
            this._selectAllButton.Name = "_selectAllButton";
            this._selectAllButton.Size = new System.Drawing.Size(75, 25);
            this._selectAllButton.TabIndex = 12;
            this._selectAllButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._selectAllButton.UseVisualStyleBackColor = true;
            this._selectAllButton.Click += new System.EventHandler(this._selectAllButton_Click);
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._myShadowLabel1);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(732, 43);
            this._myFlowLayoutPanel1.TabIndex = 0;
            // 
            // _myShadowLabel1
            // 
            this._myShadowLabel1.Angle = 0F;
            this._myShadowLabel1.AutoSize = true;
            this._myShadowLabel1.EndColor = System.Drawing.Color.Transparent;
            this._myShadowLabel1.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._myShadowLabel1.ForeColor = System.Drawing.Color.LightSkyBlue;
            this._myShadowLabel1.Location = new System.Drawing.Point(439, 5);
            this._myShadowLabel1.Name = "_myShadowLabel1";
            this._myShadowLabel1.ShadowColor = System.Drawing.Color.Black;
            this._myShadowLabel1.Size = new System.Drawing.Size(290, 33);
            this._myShadowLabel1.StartColor = System.Drawing.Color.Transparent;
            this._myShadowLabel1.TabIndex = 0;
            this._myShadowLabel1.Text = "Import Form Design";
            this._myShadowLabel1.XOffset = 1F;
            this._myShadowLabel1.YOffset = 1F;
            // 
            // _progressbar
            // 
            this._progressbar.Location = new System.Drawing.Point(134, 3);
            this._progressbar.Name = "_progressbar";
            this._progressbar.Size = new System.Drawing.Size(352, 25);
            this._progressbar.TabIndex = 13;
            this._progressbar.Visible = false;
            // 
            // _statusLabel
            // 
            this._statusLabel.AutoSize = true;
            this._statusLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._statusLabel.Location = new System.Drawing.Point(128, 0);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this._statusLabel.ResourceName = "";
            this._statusLabel.Size = new System.Drawing.Size(0, 29);
            this._statusLabel.TabIndex = 14;
            // 
            // _timer
            // 
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // _ImportFormDesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myPanel1);
            this.Name = "_ImportFormDesign";
            this.Size = new System.Drawing.Size(732, 457);
            this._myPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel2.ResumeLayout(false);
            this._myFlowLayoutPanel2.PerformLayout();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib._myShadowLabel _myShadowLabel1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel2;
        private MyLib.VistaButton _buttonClose;
        private MyLib.VistaButton _buttonProcess;
        private MyLib.VistaButton _selectAllButton;
        private MyLib._myGrid _dataGrid;
        private System.Windows.Forms.ProgressBar _progressbar;
        private MyLib._myLabel _statusLabel;
        private System.Windows.Forms.Timer _timer;
    }
}
