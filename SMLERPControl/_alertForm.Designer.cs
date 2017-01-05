namespace SMLERPControl
{
    partial class _alertForm
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
            this._grid = new MyLib._myGrid();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this.vistaButton1 = new MyLib.VistaButton();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _grid
            // 
            this._grid._extraWordShow = true;
            this._grid._selectRow = -1;
            this._grid.BackColor = System.Drawing.SystemColors.Window;
            this._grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._grid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._grid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.Font = new System.Drawing.Font("Tahoma", 9F);
            this._grid.Location = new System.Drawing.Point(5, 5);
            this._grid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._grid.Name = "_grid";
            this._grid.Size = new System.Drawing.Size(776, 342);
            this._grid.TabIndex = 2;
            this._grid.TabStop = false;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this.vistaButton1);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(5, 347);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Padding = new System.Windows.Forms.Padding(1, 1, 2, 1);
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(776, 33);
            this._myFlowLayoutPanel1.TabIndex = 3;
            // 
            // vistaButton1
            // 
            this.vistaButton1._drawNewMethod = false;
            this.vistaButton1.BackColor = System.Drawing.Color.Transparent;
            this.vistaButton1.ButtonText = "Close";
            this.vistaButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.vistaButton1.Location = new System.Drawing.Point(700, 4);
            this.vistaButton1.myImage = global::SMLERPControl.Properties.Resources.error;
            this.vistaButton1.Name = "vistaButton1";
            this.vistaButton1.Size = new System.Drawing.Size(70, 25);
            this.vistaButton1.TabIndex = 0;
            this.vistaButton1.Text = "vistaButton1";
            this.vistaButton1.UseVisualStyleBackColor = false;
            // 
            // _alertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 385);
            this.Controls.Add(this._grid);
            this.Controls.Add(this._myFlowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "_alertForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "_alertForm";
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib.VistaButton vistaButton1;
        public MyLib._myGrid _grid;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
    }
}