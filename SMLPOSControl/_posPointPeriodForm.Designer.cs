namespace SMLPOSControl
{
    partial class _posPointPeriodForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this._grid = new MyLib._myGrid();
            this._screen = new MyLib._myScreen();
            this._gridSpecialDay = new MyLib._myGrid();
            this._saveButton1 = new MyLib.VistaButton();
            this._closeButton = new MyLib.VistaButton();
            this.flowLayoutPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this._saveButton1);
            this.flowLayoutPanel1.Controls.Add(this._closeButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 597);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(973, 30);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._gridSpecialDay);
            this.splitContainer1.Size = new System.Drawing.Size(973, 597);
            this.splitContainer1.SplitterDistance = 286;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._grid);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.splitContainer2.Panel2.Controls.Add(this._screen);
            this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(5);
            this.splitContainer2.Size = new System.Drawing.Size(973, 286);
            this.splitContainer2.SplitterDistance = 416;
            this.splitContainer2.TabIndex = 0;
            // 
            // _grid
            // 
            this._grid._extraWordShow = true;
            this._grid._selectRow = -1;
            this._grid.BackColor = System.Drawing.SystemColors.Window;
            this._grid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._grid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._grid.Location = new System.Drawing.Point(0, 0);
            this._grid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._grid.Name = "_grid";
            this._grid.Size = new System.Drawing.Size(414, 284);
            this._grid.TabIndex = 0;
            this._grid.TabStop = false;
            // 
            // _screen
            // 
            this._screen._isChange = false;
            this._screen.BackColor = System.Drawing.Color.Transparent;
            this._screen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._screen.Location = new System.Drawing.Point(5, 5);
            this._screen.Name = "_screen";
            this._screen.Size = new System.Drawing.Size(541, 274);
            this._screen.TabIndex = 0;
            // 
            // _gridSpecialDay
            // 
            this._gridSpecialDay._extraWordShow = true;
            this._gridSpecialDay._selectRow = -1;
            this._gridSpecialDay.BackColor = System.Drawing.SystemColors.Window;
            this._gridSpecialDay.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridSpecialDay.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridSpecialDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridSpecialDay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._gridSpecialDay.Location = new System.Drawing.Point(0, 0);
            this._gridSpecialDay.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridSpecialDay.Name = "_gridSpecialDay";
            this._gridSpecialDay.Size = new System.Drawing.Size(971, 305);
            this._gridSpecialDay.TabIndex = 1;
            this._gridSpecialDay.TabStop = false;
            // 
            // _saveButton1
            // 
            this._saveButton1.AutoSize = true;
            this._saveButton1.BackColor = System.Drawing.Color.Transparent;
            this._saveButton1.ButtonText = "Save";
            this._saveButton1.Location = new System.Drawing.Point(908, 3);
            this._saveButton1.myImage = global::SMLPOSControl.Properties.Resources.disk_blue;
            this._saveButton1.Name = "_saveButton1";
            this._saveButton1.Size = new System.Drawing.Size(62, 24);
            this._saveButton1.TabIndex = 0;
            this._saveButton1.Text = "Save";
            this._saveButton1.UseVisualStyleBackColor = false;
            this._saveButton1.Click += new System.EventHandler(this._saveButton1_Click);
            // 
            // _closeButton
            // 
            this._closeButton.AutoSize = true;
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.ButtonText = "Close";
            this._closeButton.Location = new System.Drawing.Point(838, 3);
            this._closeButton.myImage = global::SMLPOSControl.Properties.Resources.error1;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(64, 24);
            this._closeButton.TabIndex = 1;
            this._closeButton.Text = "Close";
            this._closeButton.UseVisualStyleBackColor = false;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _posPointPeriodForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 627);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_posPointPeriodForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "กำหนดอัตราแต้มสะสม";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myGrid _grid;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MyLib.VistaButton _saveButton1;
        private MyLib.VistaButton _closeButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private MyLib._myScreen _screen;
        private MyLib._myGrid _gridSpecialDay;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}