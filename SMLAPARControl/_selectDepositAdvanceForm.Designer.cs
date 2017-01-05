namespace SMLERPAPARControl
{
    partial class _selectDepositAdvanceForm
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
            this._resultGrid = new MyLib._myGrid();
            this._myPanel1 = new MyLib._myPanel();
            this._myFlowLayoutPanel2 = new MyLib._myFlowLayoutPanel();
            this._closeButton = new MyLib._myButton();
            this._processButton = new MyLib._myButton();
            this._selectAllButton = new MyLib._myButton();
            this._myPanel1.SuspendLayout();
            this._myFlowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _resultGrid
            // 
            this._resultGrid._extraWordShow = true;
            this._resultGrid._selectRow = -1;
            this._resultGrid.BackColor = System.Drawing.SystemColors.Window;
            this._resultGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._resultGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._resultGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._resultGrid.Location = new System.Drawing.Point(0, 0);
            this._resultGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._resultGrid.Name = "_resultGrid";
            this._resultGrid.Size = new System.Drawing.Size(890, 399);
            this._resultGrid.TabIndex = 1;
            this._resultGrid.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.AutoSize = true;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel2);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 399);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(890, 32);
            this._myPanel1.TabIndex = 2;
            // 
            // _myFlowLayoutPanel2
            // 
            this._myFlowLayoutPanel2.AutoSize = true;
            this._myFlowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel2.Controls.Add(this._closeButton);
            this._myFlowLayoutPanel2.Controls.Add(this._processButton);
            this._myFlowLayoutPanel2.Controls.Add(this._selectAllButton);
            this._myFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this._myFlowLayoutPanel2.Name = "_myFlowLayoutPanel2";
            this._myFlowLayoutPanel2.Size = new System.Drawing.Size(890, 32);
            this._myFlowLayoutPanel2.TabIndex = 35;
            // 
            // _closeButton
            // 
            this._closeButton.AutoSize = true;
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.ButtonText = "ปิดหน้าจอ";
            this._closeButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._closeButton.Location = new System.Drawing.Point(801, 3);
            this._closeButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this._closeButton.myImage = global::SMLERPAPARControl.Properties.Resources.delete2;
            this._closeButton.myTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._closeButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._closeButton.myUseVisualStyleBackColor = false;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this._closeButton.ResourceName = "ปิดหน้าจอ";
            this._closeButton.Size = new System.Drawing.Size(87, 24);
            this._closeButton.TabIndex = 29;
            this._closeButton.Text = "Login";
            this._closeButton.UseVisualStyleBackColor = false;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _processButton
            // 
            this._processButton.AutoSize = true;
            this._processButton.BackColor = System.Drawing.Color.Transparent;
            this._processButton.ButtonText = "ประมวลผล";
            this._processButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._processButton.Location = new System.Drawing.Point(706, 3);
            this._processButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this._processButton.myImage = global::SMLERPAPARControl.Properties.Resources.flash;
            this._processButton.myTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._processButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._processButton.myUseVisualStyleBackColor = false;
            this._processButton.Name = "_processButton";
            this._processButton.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this._processButton.ResourceName = "ประมวลผล";
            this._processButton.Size = new System.Drawing.Size(91, 24);
            this._processButton.TabIndex = 30;
            this._processButton.Text = "Login";
            this._processButton.UseVisualStyleBackColor = false;
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _selectAllButton
            // 
            this._selectAllButton.AutoSize = true;
            this._selectAllButton.BackColor = System.Drawing.Color.Transparent;
            this._selectAllButton.ButtonText = "เลือกทั้งหมด";
            this._selectAllButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._selectAllButton.Location = new System.Drawing.Point(602, 3);
            this._selectAllButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this._selectAllButton.myImage = global::SMLERPAPARControl.Properties.Resources.checks;
            this._selectAllButton.myTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._selectAllButton.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._selectAllButton.myUseVisualStyleBackColor = false;
            this._selectAllButton.Name = "_selectAllButton";
            this._selectAllButton.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this._selectAllButton.ResourceName = "เลือกทั้งหมด";
            this._selectAllButton.Size = new System.Drawing.Size(100, 24);
            this._selectAllButton.TabIndex = 31;
            this._selectAllButton.Text = "Login";
            this._selectAllButton.UseVisualStyleBackColor = false;
            this._selectAllButton.Click += new System.EventHandler(this._selectAllButton_Click);
            // 
            // _selectDepositAdvanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 431);
            this.ControlBox = false;
            this.Controls.Add(this._resultGrid);
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "_selectDepositAdvanceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_selectForm";
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myFlowLayoutPanel2.ResumeLayout(false);
            this._myFlowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel2;
        private MyLib._myButton _closeButton;
        private MyLib._myButton _selectAllButton;
        public MyLib._myButton _processButton;
        public MyLib._myGrid _resultGrid;
    }
}